using System.ComponentModel.DataAnnotations;
using AuthText = FornoPizza.Localization.Auth;

namespace FornoPizza.Models.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = nameof(AuthText.Error_Required), ErrorMessageResourceType = typeof(AuthText))]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessageResourceName = nameof(AuthText.Error_Required), ErrorMessageResourceType = typeof(AuthText))]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessageResourceName = nameof(AuthText.Error_Required), ErrorMessageResourceType = typeof(AuthText))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(AuthText.Error_PasswordMismatch), ErrorMessageResourceType = typeof(AuthText))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
