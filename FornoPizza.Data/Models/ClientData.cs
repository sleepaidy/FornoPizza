using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class ClientData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(20)]
        public string TelephoneNumber { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        public virtual List<OrderData> Orders { get; set; } = new();
    }
}
