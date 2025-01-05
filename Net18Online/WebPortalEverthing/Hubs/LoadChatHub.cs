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
        private LoadUserService _loadUserService;
        private ILoadChatMessageRepositryReal _loadChatMessageRepositry;

        public LoadChatHub(
            LoadAuthService loadAuthService,
            LoadUserService loadUserService,
            ILoadChatMessageRepositryReal loadChatMessageRepositry)
        {
            _loadAuthService = loadAuthService;
            _loadUserService = loadUserService;
            _loadChatMessageRepositry = loadChatMessageRepositry;
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

        public void AddNewMessageToAdmin(string message)
        {
            var fromUserId = _loadAuthService.GetUserId();
            if (!fromUserId.HasValue)
            {
                throw new InvalidOperationException("Пользователь не авторизован.");
            }

            var userName = _loadAuthService.GetName();
            var formattedMessage = $"{userName}: {message}";

            //        var whoId = _loadUserService.GetUserId("admin");
            // Отправляем сообщение админу с ID = 1
            SendMessage(formattedMessage, fromUserId.Value, 1);
        }

        private void SendMessage(string message)
        {
            var userId = _loadAuthService.GetUserId();
            _loadChatMessageRepositry.AddMessage(userId, message);
            Clients.All.NewMessageAdded(message).Wait();
        }

        private void SendMessage(string message, int whoId)
        {
            var userId = _loadAuthService.GetUserId();
            _loadChatMessageRepositry.AddMessage(userId, message);

            // whoId преобразуется в строку, чтобы соответствовать UserId в SignalR
            Clients.User(whoId.ToString()).NewMessageAdded(message).Wait();
        }

        private void SendMessage(string message, int fromUserId, int? toUserId = null)
        {
            _loadChatMessageRepositry.AddMessage(fromUserId, message, toUserId);

            if (toUserId.HasValue)
            {
                // Отправляем сообщение только определённому пользователю
                Clients.User(toUserId.Value.ToString()).NewMessageAdded(message).Wait();
            }
            else
            {
                // Отправляем сообщение всем
                Clients.All.NewMessageAdded(message).Wait();
            }
        }


    }
}
