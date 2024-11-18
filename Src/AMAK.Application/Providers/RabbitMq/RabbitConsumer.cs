using System.Text;
using Newtonsoft.Json;
using AMAK.Application.Providers.Mail.Dtos;
using AMAK.Application.Providers.RabbitMq.Common;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Constants;
using Microsoft.Extensions.DependencyInjection;
using AMAK.Application.Providers.Cloudinary;
using AMAK.Application.Interfaces;
using Microsoft.Extensions.Logging;
using AMAK.Application.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Providers.RabbitMq {
    public class RabbitConsumer : IHostedService {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private IConnection? _connection;
        private IModel? _channel;
        private readonly string RabbitHost;


        public RabbitConsumer(IServiceProvider serviceProvider, Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<RabbitConsumer> logger) {
            RabbitHost = configuration["RabbitConfig:HostName"]!;
            _serviceProvider = serviceProvider;
            _logger = logger;

        }

        public Task StartAsync(CancellationToken cancellationToken) {
            var factory = new ConnectionFactory() { HostName = RabbitHost };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            DeclareAndConsumeQueue(RabbitQueue.OrderQueue);
            DeclareAndConsumeQueue(RabbitQueue.MailQueue);
            UploadQueueHandle(RabbitQueue.Upload);

            return Task.CompletedTask;
        }

        private void UploadQueueHandle(string queue) {
            _channel?.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try {
                    using var scope = _serviceProvider.CreateScope();

                    var cloudinaryService = scope.ServiceProvider.GetRequiredService<ICloudinaryService>();

                    var photoRepository = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Models.Photo>>();

                    var json = JsonConvert.DeserializeObject<RabbitUpload>(message) ?? throw new BadRequestException("Message missing!");

                    var fileConverts = new List<IFormFile>();

                    var photos = new List<Domain.Models.Photo>();


                    foreach (var file in json.Files) {
                        var photo = Util.ConvertBase64ToImage(file);

                        var upload = await cloudinaryService.UploadPhotoAsync(photo);

                        if (upload.Error != null) {
                            throw new BadRequestException(message: upload.Error.Message);
                        }

                        var newPhoto = new Domain.Models.Photo() {
                            Id = Guid.NewGuid(),
                            Url = upload.SecureUrl.AbsoluteUri,
                            PublicId = upload.PublicId,
                            ProductId = json.ProductId,
                        };

                        photos.Add(newPhoto);
                    }

                    photoRepository.AddRange(photos);

                    await photoRepository.SaveChangesAsync();

                } catch (JsonException ex) {
                    throw new BadRequestException($"Error deserializing message: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }


        private void DeclareAndConsumeQueue(string queue) {
            _channel?.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try {
                    using var scope = _serviceProvider.CreateScope();
                    var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();

                    switch (queue) {
                        case RabbitQueue.OrderQueue:
                            var order = JsonConvert.DeserializeObject<OrderMailEvent>(message);
                            if (order != null) {
                                await mailService.SendOrderMail(order);
                            }
                            break;

                        case RabbitQueue.MailQueue:
                            var mailMessage = JsonConvert.DeserializeObject<MailWithTokenEvent>(message);
                            if (mailMessage != null) {
                                await ProcessMailMessage(mailService, mailMessage);
                            }
                            break;

                        default:
                            break;
                    }

                } catch (JsonException ex) {
                    throw new BadRequestException($"Error deserializing message: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }

        private static async Task ProcessMailMessage(IMailService mailService, MailWithTokenEvent mailMessage) {
            if (mailMessage.Type == EEmailType.CONFIRM_ACCOUNT) {
                await mailService.SendEmailConfirmationAccount(mailMessage);
            } else if (mailMessage.Type == EEmailType.FORGOT_PASSWORD) {
                await mailService.SendMailResetPassword(mailMessage);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _channel?.Close();
            _connection?.Close();
            return Task.CompletedTask;
        }
    }

}
