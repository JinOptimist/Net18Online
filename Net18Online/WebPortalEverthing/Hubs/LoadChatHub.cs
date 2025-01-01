using Everything.Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Services;
using WebPortalEverthing.Services.LoadTesting;

namespace WebPortalEverthing.Hubs
{
    public interface ILoadChatHub
    {
        Task NewMessageAdded(string message);
    }

    public class LoadChatHub : Hub<ILoadChatHub>
    {
        private LoadAuthService _loadAuthService;
        private IChatMessageRepositryReal _chatMessageRepositry;

        public LoadChatHub(LoadAuthService loadAuthService, IChatMessageRepositryReal chatMessageRepositry)
        {
            _loadAuthService = loadAuthService;
            _chatMessageRepositry = chatMessageRepositry;
        }

        public void UserEnteredToChat()
        {
            var userName = _loadAuthService.GetName();

            var newMessage = $"{userName} вошёл в чат";

            SendMessage(newMessage);
        }

        public void AddNewMessage(string message)
        {
            var userName = _loadAuthService.GetName();

            var newMessage = $"{userName}: {message}";

            SendMessage(newMessage);
        }

        private void SendMessage(string message)
        {
            var userId = _loadAuthService.GetUserId();
            _chatMessageRepositry.AddMessage(userId, message);
            Clients.All.NewMessageAdded(message).Wait();
        }
        
        public void UserCreatedNewPost()
        {
            var userName = _loadAuthService.GetName();

            var newMessage = $"{userName} add new post";

            SendMessage(newMessage);
        }
    }
}
