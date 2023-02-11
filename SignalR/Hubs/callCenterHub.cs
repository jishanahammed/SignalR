using Microsoft.AspNetCore.SignalR;
using SignalR.Models;

namespace SignalR.Hubs
{
    public class callCenterHub:Hub
    {
        public async Task NewcallRecive(call newcall)
        {
            await Clients.All.SendAsync("newCallReceived", newcall);
        }
    }
}
