using AutoMapper;
using Datingapp.API.Data;
using Datingapp.API.DTO;
using Datingapp.API.Extensions;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Datingapp.API.SignalR
{
    public class MessageHub :Hub
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IMessageRepository messageRepository;
        //private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IHubContext<PresenceHub> presenceHub;
        private readonly PresenceTracker tracker;

        public MessageHub(IUnitOfWork unitOfWork, IMapper mapper,
             IHubContext<PresenceHub> presenceHub,
            PresenceTracker tracker) {
            //this.messageRepository = messageRepository;
            //this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.presenceHub = presenceHub;
            this.tracker = tracker;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group =await AddToGroup(groupName);
            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

                var messages = await unitOfWork.MessageRepository.
                GetMessageThread(Context.User.GetUsername(), otherUser);

            if (unitOfWork.hasChanges()) await unitOfWork.Complete();

                await Clients.Caller.SendAsync("ReceiveMessageThread", messages);

                Console.WriteLine($"Sent message thread to group {groupName}");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group =await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }


    public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUsername();
            //var groupName = GetGroupName(username, createMessageDto.RecipientUsername);

            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("Cannot send message to self");

            var sender = await unitOfWork.UserRepository.GetByUsername(username);
            var recipient = await unitOfWork.UserRepository.GetByUsername(createMessageDto.RecipientUsername);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await unitOfWork.MessageRepository.GetMessageGroup(groupName);
            if(group.Connections.Any(x=>x.Username == recipient.UserName))
            {
                Console.WriteLine(recipient.UserName);
                message.DateRead = DateTime.Now;
            }
            else
            {
                var connections = await tracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    //checking if they are connected
                    await presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.UserName, knownAs = sender.KnownAs });
                }
            }
            unitOfWork.MessageRepository.AddMessage(message);

            if (await unitOfWork.Complete()) {
                Console.WriteLine($"Sending message to group {groupName}");
                await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
            }
        }

         private async Task<Group> AddToGroup(string groupName)
        {
            var group = await unitOfWork.MessageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new Group(groupName);
                unitOfWork.MessageRepository.AddGroup(group);

            }
            //if (!group.Connections.Any(c => c.ConnectionId == connection.ConnectionId))
            //{
            //    group.Connections.Add(connection);
            //    await messageRepository.SaveAllAsync();
            //    Console.WriteLine($"Added connection {connection.ConnectionId} to group {groupName}");
            //    return true;
            //}

            //Console.WriteLine($"Connection {connection.ConnectionId} is already in group {groupName}");
            //return false;
            group.Connections.Add(connection);
            if (await unitOfWork.Complete()) return group;

            throw new HubException("Failed to join group");
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await unitOfWork.MessageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection =group.Connections.FirstOrDefault(x=>x.ConnectionId==Context.ConnectionId);
            unitOfWork.MessageRepository.RemoveConnection(connection);
            if (await unitOfWork.Complete()) return group;

            throw new HubException("Failed to remove from group");
        }
        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller,other)<0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
