using Microsoft.AspNetCore.SignalR;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Hubs
{
    public interface IGameAlertHub 
    {
        Task Alert(string message);
    }

    public class GameAlertHub : Hub<IGameAlertHub>
    {
        private AuthService _authService;

        public GameAlertHub(AuthService authService)
        {
            _authService = authService;
        }

        public void userEnteredToSite()
        {
            var userName = _authService.GetName();

            var newMessage = $"{userName} зашёл на сайт";

            Clients.All.Alert(newMessage).Wait();

        }

    }
}
