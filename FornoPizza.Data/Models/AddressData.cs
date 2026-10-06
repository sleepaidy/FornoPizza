using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class AddressData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        public virtual UserData? User { get; set; }
    }
}
