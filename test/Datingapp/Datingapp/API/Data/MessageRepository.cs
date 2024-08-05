using Datingapp.API.DTO;
using Datingapp.API.Helpers;
using Datingapp.API.Interface;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext context;

        public MessageRepository(DataContext context)
        {
            this.context = context;
        }
        public void AddMessage(Message message)
        {
            context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
           context.Messages.Remove(message);
        }

        public Task<IEnumerable<MessageDto>> GetMessaeThread(int currentUserId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetMessage(int id)
        {
            return await context.Messages.FindAsync(id);
        }

        public Task<PagedList<MessageDto>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
