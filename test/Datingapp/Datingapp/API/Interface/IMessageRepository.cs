using Datingapp.API.DTO;
using Datingapp.API.Helpers;
using Datingapp.API.Models;

namespace Datingapp.API.Interface
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser();
        Task<IEnumerable<MessageDto>> GetMessaeThread(int currentUserId, int recipientId);
        Task<bool> SaveAllAsync();
    }
}
