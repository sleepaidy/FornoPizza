using FornoPizza.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class UserData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }

        public virtual List<AddressData> Addresses { get; set; } = new();
    }
}
