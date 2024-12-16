using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface ICoffeChatMessageRepositryKey : ICoffeChatMessageRepositry<CoffeChatMessageData>
    {
        void AddMessage(int? userId, string message);
    }
    public class CoffeChatMessageRepository : BaseRepository<CoffeChatMessageData>, ICoffeChatMessageRepositryKey
    {
        public const int COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM = 2;
        public CoffeChatMessageRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void AddMessage(int? userId, string message)
        {
            var isMessageDuplicate = _dbSet
                .OrderByDescending(x => x.CreationTime)
                .Take(COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM)
                .Any(x => x.Message == message);

            if (isMessageDuplicate)
            {
                return;
            }

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
    }
}
