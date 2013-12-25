using System.ComponentModel.DataAnnotations;

namespace TwitterCopy.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [RegularExpression("^@", ErrorMessage = "The username shoudn't contain @ symbol")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(80)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}