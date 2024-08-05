using AutoMapper;
using Datingapp.API.DTO;
using Datingapp.API.Extensions;
using Datingapp.API.Interface;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Datingapp.API.Controllers
{
    [Authorize]
    public class MessagesController: BaseApiController
    {
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public MessagesController( IUserRepository userRepository,
            IMessageRepository messageRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>>CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername();

            if(username== createMessageDto.RecipientUsername.ToLower())
                return BadRequest("Cannot send message to self");

            var sender = await userRepository.GetByUsername(username);
            var recipient = await userRepository.GetByUsername(createMessageDto.RecipientUsername);

            if (recipient == null) return NotFound();

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            messageRepository.AddMessage(message);

            if (await messageRepository.SaveAllAsync()) return Ok(mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send message");
        }
    }
}
