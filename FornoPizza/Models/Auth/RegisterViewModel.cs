using System.ComponentModel.DataAnnotations;

namespace FornoPizza.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
