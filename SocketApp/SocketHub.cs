using Microsoft.AspNetCore.SignalR;

namespace SocketApp
{
    public class SocketHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Receive", message);
        }
    }
}
