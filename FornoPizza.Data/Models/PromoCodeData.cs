using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Data.Models
{
    public class PromoCodeData
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
