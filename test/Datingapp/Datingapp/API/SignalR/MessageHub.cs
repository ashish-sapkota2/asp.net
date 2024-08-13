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
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public MessageHub(IMessageRepository messageRepository, IMapper mapper,
            IUserRepository userRepository) {
            this.messageRepository = messageRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public override async Task OnConnectedAsync()
        {
          try{  var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages = await messageRepository.
                GetMessageThread(Context.User.GetUsername(), otherUser);

                await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
            }catch(Exception ex)
            {
                Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
                throw;
            }
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        //public class MessageHub : Hub
//{
//    public async Task SendMessage(string recipientUsername, string content)
//        {
//            // Logic to store the message

//            // Fetch updated message thread
//            var messages = await messageRepository.GetMessageThread(Context.User.GetUsername(), recipientUsername);

//            // Notify the recipient
//            await Clients.User(recipientUsername).SendAsync("ReceiveMessageThread", messages);
//        }
//    }

    public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("Cannot send message to self");

            var sender = await userRepository.GetByUsername(username);
            var recipient = await userRepository.GetByUsername(createMessageDto.RecipientUsername);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            messageRepository.AddMessage(message);

            if (await messageRepository.SaveAllAsync()) {
                var group = GetGroupName(sender.UserName, recipient.UserName);
                await Clients.Group(group).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
            }
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller,other)<0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
