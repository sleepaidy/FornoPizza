using FornoPizza.Services;
using Microsoft.AspNetCore.SignalR;

namespace FornoPizza.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(AuthService.COOKIE_ID_KEY)?.Value;
        }
    }
}
