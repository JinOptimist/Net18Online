using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface ICoffeChatMessageRepositryKey : ICoffeChatMessageRepositry<CoffeChatMessageData>
    {
        void AddMessage(int? userId, string message);
        PaginatedMessages GetAllMessages(int pageNumber, int pageSize);
    }
    public class CoffeChatMessageRepository : BaseRepository<CoffeChatMessageData>, ICoffeChatMessageRepositryKey
    {
        public const int COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM = 2;
        public CoffeChatMessageRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void AddMessage(int? userId, string message)
        {
            var messageData = new CoffeChatMessageData
            {
                CreationTime = DateTime.Now,
                Message = message,
                User = !userId.HasValue
                    ? null
                    : _webDbContext.Users.First(x => x.Id == userId)
            };

            base.Add(messageData);
        }

        public PaginatedMessages GetAllMessages(int pageNumber, int pageSize)
        {
            var totalCount = _dbSet.Count();
            var messages = _dbSet
                .OrderByDescending(x => x.CreationTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => m.Message)
                .ToList();

            return new PaginatedMessages
            {
                Messages = messages,
                TotalCount = totalCount
            };
        }
    }
}
