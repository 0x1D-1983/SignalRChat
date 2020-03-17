using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static Dictionary<Guid, string> users = new Dictionary<Guid, string>();

        public override Task OnConnectedAsync()
        {
            var connectionId = new Guid(Context.ConnectionId);

            var user = Context.User;

            string username;
            if (!users.TryGetValue(connectionId, out username))
            {
                // todo: register user connection

                // add
                users.Add(connectionId, user.Identity.Name);

            }

            return base.OnConnectedAsync();

            
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = new Guid(Context.ConnectionId);

            string username;
            if (users.TryGetValue(connectionId, out username))
            {
                //todo: de-register

                // remove
                users.Remove(connectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
