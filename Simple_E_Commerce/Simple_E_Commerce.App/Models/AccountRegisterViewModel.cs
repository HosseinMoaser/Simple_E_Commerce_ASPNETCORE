using System.ComponentModel.DataAnnotations;

namespace Simple_E_Commerce.App.Models
{
    public class AccountRegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Enter {0} in correct mode")]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name ="Re-Type Password")]
        public string RePassword { get; set; }
    }
}
