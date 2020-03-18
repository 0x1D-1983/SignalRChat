using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static ConcurrentDictionary<Guid, string> users = new ConcurrentDictionary<Guid, string>();

        public override Task OnConnectedAsync()
        {
            var connectionId = new Guid(Context.ConnectionId);
            var user = Context.User;

            if (users.TryAdd(connectionId, user.Identity.Name))
            {
                // todo: register user connection
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = new Guid(Context.ConnectionId);


            string username;
            
            if (users.TryRemove(connectionId, out username))
            {
                //todo: de-register
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
