using Microsoft.AspNetCore.SignalR;


namespace WebPortalEverthing.Providers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Получаем UserId из ClaimsPrincipal
            return connection.User?.FindFirst("UserId")?.Value;
        }
    }
}