namespace TwitterCopy.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ForgottenPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage="Password is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Password Confirm")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string PasswordConfirm { get; set; }

        public Guid Token { get; set; }
    }
}