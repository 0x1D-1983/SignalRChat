using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static ImmutableDictionary<Guid, string> users = ImmutableDictionary<Guid, string>.Empty;

        public override Task OnConnectedAsync()
        {
            var connectionId = new Guid(Context.ConnectionId);
            var user = Context.User;

            if (ImmutableInterlocked.TryAdd(ref users, connectionId, user.Identity.Name))
            {
                // todo: register user connection
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = new Guid(Context.ConnectionId);


            string username;
            
            if (ImmutableInterlocked.TryRemove(ref users, connectionId, out username))
            {
                //todo: de-register
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
