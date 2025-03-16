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

        public async Task AddNewMessage(string message)
        {
            var userName = _authService.GetName();

            var newMessage = $"{userName}: {message}";

            await SendMessage(newMessage);
        }

        private async Task SendMessage(string message)
        {
            var userId = _authService.GetUserId();
            _coffeChatMessageRepository.AddMessage(userId, message);
            await Clients.All.NewMessageAdded(message);
        }
    }
}
