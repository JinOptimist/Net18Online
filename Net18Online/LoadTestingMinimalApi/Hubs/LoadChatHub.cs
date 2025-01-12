using LoadTestingMinimalApi.DBstuff;
using LoadTestingMinimalApi.DBstuff.DataModels;
using Microsoft.AspNetCore.SignalR;

namespace LoadTestingMinimalApi.Hubs
{
    public interface ILoadChatHub
    {
        Task NewMessageAdded(string message);
    }

    public class LoadChatHub : Hub<ILoadChatHub>
    {

        private ChatDbContext _chatDbContext;

        public LoadChatHub(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public void UserEnteredToChat(int authorId, string authorName)
        {
            var newMessage = $"{authorName} вошёл в чат";
            SendMessage(authorId, authorName, newMessage);
        }

        public void AddNewMessage(int authorId, string authorName, string message)
        {
            var newMessage = $"{authorName}: {message}";
            SendMessage(authorId, authorName, newMessage);
        }

        private void SendMessage(int authorId, string authorName, string messageText)

        {
            var message = new Message
            {
                AuthorId = authorId,
                AuthorName = authorName,
                Text = messageText,
            };
            _chatDbContext.Add(message);
            _chatDbContext.SaveChanges();

            Clients.All.NewMessageAdded(messageText).Wait();
        }




    }
}
