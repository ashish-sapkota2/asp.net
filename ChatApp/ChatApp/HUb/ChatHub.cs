using Microsoft.AspNetCore.SignalR;

namespace ChatApp.HUb
{
    public class ChatHub : Hub
    {
        private readonly IDictionary<string, UserRoomConnection> _connection;

        public ChatHub(IDictionary<string, UserRoomConnection> connection)
        {
            _connection = connection;
        }

        public async Task JoinRoom(UserRoomConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName: userConnection.room!);
            _connection[Context.ConnectionId] = userConnection;
            await Clients.Group(userConnection.room!)
               .SendAsync(method: "ReceiveMessage", arg1: "Chat Bot", arg2: $"{userConnection.user} has joined the Group", arg3:DateTime.Now);
            await SendConnectedUser(userConnection.room);
        }
        
        public async Task SendMessage(string message)
        {
            if(_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
            {
                await Clients.Group(userRoomConnection.room!)
                    .SendAsync(method:"ReceiveMessage", arg1:userRoomConnection.user, arg2:message, arg3:DateTime.Now);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(!_connection.TryGetValue(Context.ConnectionId,out UserRoomConnection roomConnection))
            {
            return base.OnDisconnectedAsync(exception);
            }
            _connection.Remove(Context.ConnectionId);
            Clients.Group(roomConnection.room!)
                .SendAsync(method:"ReceiveMessage", arg1:"Chat Bot", arg2:$"{roomConnection.user} has left the group",arg3:DateTime.Now);
            SendConnectedUser(roomConnection.room);
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendConnectedUser(string room)
        {
            var users = _connection.Values
                .Where(u => u.room == room)
                .Select(s => s.user);
            return Clients.Group(room).SendAsync("ConnectedUser", users);
        }
    }
}
