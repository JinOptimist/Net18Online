using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Hubs
{
    public interface ICoffeShopChatHub
    {
        Task NewMessageAdded(string message);
    }

    public class CoffeShopChatHub : Hub<ICoffeShopChatHub>
    {
        private AuthService _authService;
        private ICoffeChatMessageRepositryKey _coffeChatMessageRepository;

        public CoffeShopChatHub(AuthService authService, ICoffeChatMessageRepositryKey coffeChatMessageRepository)
        {
            _authService = authService;
            _coffeChatMessageRepository = coffeChatMessageRepository;
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
            _coffeChatMessageRepository.AddMessage(userId, message);
            Clients.All.NewMessageAdded(message).Wait();
        }
    }
}
