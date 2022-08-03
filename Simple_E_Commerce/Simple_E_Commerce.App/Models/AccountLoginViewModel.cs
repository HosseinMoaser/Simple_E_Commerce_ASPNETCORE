using System.ComponentModel.DataAnnotations;

namespace Simple_E_Commerce.App.Models
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Enter {0} in correct mode")]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
