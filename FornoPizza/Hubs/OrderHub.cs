using FornoPizza.Hubs.Interfaces;
using FornoPizza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FornoPizza.Hubs
{
    [Authorize(AuthenticationSchemes = AuthService.AUTH_KEY)]
    public class OrderHub : Hub<IOrderHub>
    {

    }
}
