using Microsoft.AspNetCore.SignalR;

namespace AMAK.Application.Providers.WS {
    public class ChatHub : Hub {

        public override async Task OnConnectedAsync() {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the chat");
        }

        public async Task SendMessage(string connectionId, string message) {

            await Clients.Client(connectionId).SendAsync("Message", message);
        }

    }
}