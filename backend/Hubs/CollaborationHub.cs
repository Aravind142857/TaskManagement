using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class CollaborationHub : Hub
    {
    public async Task NotifyTaskUpdated(string taskId, object updatedTask)
    {
        await Clients.All.SendAsync("TaskUpdated", taskId, updatedTask);
    }

    public async Task NotifyUserJoined(string userId)
    {
        await Clients.All.SendAsync("UserJoined", userId);
    }

    public async Task NotifyUserLeft(string userId)
    {
        await Clients.All.SendAsync("UserLeft", userId);
    }
    }
}