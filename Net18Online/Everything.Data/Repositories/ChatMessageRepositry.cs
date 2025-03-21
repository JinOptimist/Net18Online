﻿using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IChatMessageRepositryReal : IChatMessageRepositry<ChatMessageData>
    {
        void AddMessage(int? userId, string message);
        List<string> GetLastMessages(int count = 5);
    }

    public class ChatMessageRepositry : BaseRepository<ChatMessageData>, IChatMessageRepositryReal
    {
        public const int COUNT_OF_MESSAGE_TO_CHECK_ON_SPAM = 3;
        public ChatMessageRepositry(WebDbContext webDbContext) : base(webDbContext)
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
                // TODO Notify that you message a spam
                return;
            }

            var messageData = new ChatMessageData
            {
                CreationTime = DateTime.Now,
                Message = message,
                User = !userId.HasValue
                    ? null
                    : _webDbContext.Users.First(x => x.Id == userId)
            };

            base.Add(messageData);
        }

        public List<string> GetLastMessages(int count = 5)
        {
            return _dbSet
                 .OrderByDescending(x => x.CreationTime)
                 .Take(count)
                 .OrderBy(x => x.CreationTime)
                 .Select(x => x.Message)
                 .ToList();
        }
    }
}
