using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class ClientData
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string TelephoneNumber { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }

        public virtual List<OrderData> Orders { get; set; } = new();
    }
}
