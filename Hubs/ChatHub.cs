using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        static Atom<ImmutableDictionary<Guid, string>> users =
            new Atom<ImmutableDictionary<Guid, string>>(ImmutableDictionary<Guid, string>.Empty);

        public override Task OnConnectedAsync()
        {
            var connectionId = new Guid(Context.ConnectionId);
            var user = Context.User;

            var temp = users.Value;

            if (users.Swap(d =>
            {
                if (d.ContainsKey(connectionId)) return d;

                return d.Add(connectionId, user.Identity.Name);
            }) != temp)
            {
                // todo: register user connection
            }

            return base.OnConnectedAsync();

            
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = new Guid(Context.ConnectionId);


            var temp = users.Value;

            if (users.Swap(d =>
            {
                if (d.ContainsKey(connectionId)) return d.Remove(connectionId);

                return d;
            }) != temp)
            {
                //todo: de-register
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
