using FornoPizza.Data.Models;

namespace FornoPizza.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        UserData? GetByNameAndPassword(string name, string password);
        bool IsNameUniq(string name);
        void Registration(UserData user);
        UserData? Get(int id);
        List<AddressData> GetAddresses(int userId);
        void RememberAddress(int userId, string address);
    }
}
