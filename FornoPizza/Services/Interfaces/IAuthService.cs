using FornoPizza.Data.Models;

namespace FornoPizza.Services.Interfaces
{
    public interface IAuthService
    {
        public Task SignInAsync(UserData user);
        public int? GetCurrentUserId();
    }
}
