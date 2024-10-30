using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Chat.Dtos;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Providers.WS {
    public class ChatHub : Hub {
        private readonly IAuthService _authService;
        private readonly IRepository<Chat> _chatRepository;

        public ChatHub(IAuthService authService, IRepository<Chat> chatRepository) {
            _authService = authService;
            _chatRepository = chatRepository;
        }

        public async Task GetAccounts(string id) {
            var accounts = await _authService.GetAllAdminMemberAsync();

            var result = new List<AccountChatResponse>();

            foreach (var account in accounts.Result) {
                var lastMessage = await _chatRepository.GetAll()
                    .Where(p => (p.FromUserId == id && p.ToUserId == account.Id) ||
                                (p.FromUserId == account.Id && p.ToUserId == id))
                    .OrderByDescending(p => p.CreateAt)
                    .FirstOrDefaultAsync();

                result.Add(new AccountChatResponse {
                    Id = account.Id,
                    Name = account.Name,
                    Avatar = account.Avatar,
                    LastMessage = lastMessage?.Content,
                });
            }

            await Clients.All.SendAsync($"Users-{id}", accounts);
        }

        public async Task JoinChat(string fromUserId, string toUserId) {
            var messages = await _chatRepository.GetAll()
                .Where(p => (p.FromUserId == fromUserId && p.ToUserId == toUserId) ||
                            (p.FromUserId == toUserId && p.ToUserId == fromUserId))
                .OrderBy(p => p.CreateAt)
                .ToListAsync();

            await Clients.Caller.SendAsync("Messages", messages);
        }

        public async Task CreateMessage(MessageRequest request) {
            var newMessage = new Chat {
                Id = Guid.NewGuid(),
                Content = request.Content,
                FromUserId = request.FromUserId,
                ToUserId = request.ToUserId,
                CreateAt = DateTime.UtcNow
            };

            _chatRepository.Add(newMessage);
            await _chatRepository.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", newMessage);
        }

        public async Task DeleteMessage(Guid messageId) {
            var message = await _chatRepository.GetById(messageId);
            if (message != null) {
                message.IsDeleted = true;
                await _chatRepository.SaveChangesAsync();

                await Clients.All.SendAsync("MessageDeleted", messageId);
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception) {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
