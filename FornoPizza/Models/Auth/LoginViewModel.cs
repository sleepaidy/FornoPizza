using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
