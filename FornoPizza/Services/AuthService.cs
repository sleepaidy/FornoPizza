using FornoPizza.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using FornoPizza.Services.Interfaces;

namespace FornoPizza.Services
{
    public class AuthService : IAuthService
    {
        public const string AUTH_KEY = "AuthNameFornoPizza";
        public const string COOKIE_ID_KEY = "Id";
        public const string COOKIE_NAME_KEY = "UserName";


        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(UserData user)
        {
            var claims = new List<Claim>
            {
                new Claim(COOKIE_ID_KEY, user.Id.ToString()),
                new Claim(COOKIE_NAME_KEY, user.Name.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.AuthenticationMethod, AUTH_KEY),
            };

            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!
                .SignInAsync(AUTH_KEY, principal);
        }

        public int? GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return null;
            }
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(COOKIE_ID_KEY); 
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }
    }
}
