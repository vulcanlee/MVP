using Microsoft.AspNetCore.SignalR;

namespace SignalrChatServer.Hubs
{
    public class MyChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
