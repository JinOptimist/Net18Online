using Microsoft.AspNetCore.SignalR;

namespace WebPortalEverthing.Hubs
{
    public interface INotificationHub
    {
        Task NewNotification(string message);
    }

    public class NotificationHub : Hub<INotificationHub>
    {
    }
}
