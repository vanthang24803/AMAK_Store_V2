using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Notification.Dtos;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AMAK.Application.Common.Exceptions;

namespace AMAK.Application.Services.Notification {
    public class NotificationService : INotificationService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Domain.Models.Notification> _notificationRepository;
        private readonly IRepository<MessageUser> _messageUserRepository;

        public NotificationService(IRepository<MessageUser> messageUserRepository, IRepository<Domain.Models.Notification> notificationRepository, UserManager<ApplicationUser> userManager) {
            _messageUserRepository = messageUserRepository;
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public async Task<Response<string>> CreateGlobalNotification(CreateGlobalNotificationRequest request) {

            var users = await _userManager.Users.ToListAsync();

            var newNotification = new Domain.Models.Notification() {
                Id = Guid.NewGuid(),
                IsGlobal = true,
                Content = request.Content,
                Url = request.Url
            };

            _notificationRepository.Add(newNotification);

            await _notificationRepository.SaveChangesAsync();

            foreach (var user in users) {
                var newMessageUser = new MessageUser() {
                    IsOpened = false,
                    NonfictionId = newNotification.Id,
                    IsSeen = false,
                    UserId = user.Id,
                };

                _messageUserRepository.Add(newMessageUser);

            }
            await _messageUserRepository.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.Created, "Global notification created!");
        }

        public async Task<Response<NotificationResponse>> CreateNotification(CreateNotificationForAccountRequest request) {

            var existingAccount = await _userManager.FindByIdAsync(request.UserId)
            ?? throw new NotFoundException("Account not found!");

            var newNotification = new Domain.Models.Notification() {
                Id = Guid.NewGuid(),
                IsGlobal = false,
                Content = request.Content,
                Url = request.Url,
            };

            _notificationRepository.Add(newNotification);

            await _notificationRepository.SaveChangesAsync();

            var newMessageUser = new MessageUser() {
                IsOpened = false,
                NonfictionId = newNotification.Id,
                IsSeen = false,
                UserId = existingAccount.Id,
            };

            _messageUserRepository.Add(newMessageUser);

            await _messageUserRepository.SaveChangesAsync();

            var response = new NotificationResponse() {
                Id = Guid.NewGuid(),
                Url = newNotification.Url,
                Content = newNotification.Content,
                IsOpened = newMessageUser.IsOpened,
                IsSeen = newMessageUser.IsSeen,
                SeenAt = newMessageUser.SeenAt,
                CreateAt = newNotification.CreateAt,
            };

            return new Response<NotificationResponse>(HttpStatusCode.Created, response);
        }

        public async Task<Response<string>> DeleteNotificationForAccount(ClaimsPrincipal user, Guid id) {
            var existingAccount = await _userManager.GetUserAsync(user)
          ?? throw new NotFoundException("Account not found!");

            var existingNotification = await _messageUserRepository.GetAll().FirstOrDefaultAsync(
              x => x.NonfictionId == id && x.UserId == existingAccount.Id && !x.IsSeen
            ) ?? throw new NotFoundException("Notification not found!");

            _messageUserRepository.Remove(existingNotification);

            await _messageUserRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Delete notification successfully!");
        }

        public async Task<Response<List<NotificationResponse>>> GetAllNotificationForAccount(ClaimsPrincipal user) {

            var existingAccount = await _userManager.GetUserAsync(user)
            ?? throw new NotFoundException("Account not found!");

            var messageUsers = await _messageUserRepository.GetAll()
                        .Where(m => m.UserId == existingAccount.Id).ToListAsync();


            var response = new List<NotificationResponse>();

            foreach (var message in messageUsers) {
                var notification = await _notificationRepository.GetById(message.NonfictionId)
                     ?? throw new NotFoundException("Notification not found");


                var item = new NotificationResponse() {
                    Id = notification.Id,
                    Content = notification.Content,
                    Url = notification.Url!,
                    CreateAt = notification.CreateAt,
                    IsOpened = message.IsOpened,
                    IsSeen = message.IsSeen,
                    SeenAt = message.SeenAt
                };

                response.Add(item);
            }

            return new Response<List<NotificationResponse>>(HttpStatusCode.OK, [.. response.OrderByDescending(x => x.CreateAt)]);

        }

        public async Task<Response<string>> OpenAllNotification(ClaimsPrincipal user) {
            var existingAccount = await _userManager.GetUserAsync(user)
            ?? throw new NotFoundException("Account not found!");

            var messageUsers = await _messageUserRepository.GetAll().Where(
                x => x.UserId == existingAccount.Id
            ).ToListAsync();

            foreach (var message in messageUsers) {
                message.IsOpened = true;
            }

            await _messageUserRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Opened All Successfully!");
        }

        public async Task<Response<string>> SeenAllNotification(ClaimsPrincipal user) {

            var existingAccount = await _userManager.GetUserAsync(user)
            ?? throw new NotFoundException("Account not found!");

            var messageUsers = await _messageUserRepository.GetAll().Where(
                x => x.UserId == existingAccount.Id
            ).ToListAsync();

            foreach (var message in messageUsers) {
                message.IsSeen = true;
                message.IsOpened = true;
                message.SeenAt = DateTime.UtcNow;
            }

            await _messageUserRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Seen All Successfully!");
        }

        public async Task<Response<string>> SendOneNotification(ClaimsPrincipal user, Guid id) {
            var existingAccount = await _userManager.GetUserAsync(user)
            ?? throw new NotFoundException("Account not found!");

            var messageUsers = await _messageUserRepository.GetAll().FirstOrDefaultAsync(
                x => x.UserId == existingAccount.Id && x.NonfictionId == id
            ) ?? throw new NotFoundException("Notification not found!");

            messageUsers.SeenAt = DateTime.UtcNow;
            messageUsers.IsOpened = true;
            messageUsers.IsSeen = true;

            await _messageUserRepository.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Seen One Successfully!");

        }
    }
}