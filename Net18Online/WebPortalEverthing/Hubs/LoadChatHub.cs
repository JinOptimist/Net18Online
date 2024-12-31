using Everything.Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Hubs
{
    public interface ILoadChatHub
    {
        Task NewMessageAdded(string message);
    }

    public class LoadChatHub : Hub<IChatHub>
    {
        private AuthService _authService;
        private IChatMessageRepositryReal _chatMessageRepositry;

        public LoadChatHub(AuthService authService, IChatMessageRepositryReal chatMessageRepositry)
        {
            _authService = authService;
            _chatMessageRepositry = chatMessageRepositry;
        }

        public void UserEnteredToChat()
        {
            var userName = _authService.GetName();

            var newMessage = $"{userName} вошёл в чат";

            SendMessage(newMessage);
        }

        public void AddNewMessage(string message)
        {
            var userName = _authService.GetName();

            var newMessage = $"{userName}: {message}";

            SendMessage(newMessage);
        }

        private void SendMessage(string message)
        {
            var userId = _authService.GetUserId();
            _chatMessageRepositry.AddMessage(userId, message);
            Clients.All.NewMessageAdded(message).Wait();
        }
        
        public void UserCreatedNewPost()
        {
            var userName = _authService.GetName();

            var newMessage = $"{userName} add new post";

            SendMessage(newMessage);
        }
    }
}
