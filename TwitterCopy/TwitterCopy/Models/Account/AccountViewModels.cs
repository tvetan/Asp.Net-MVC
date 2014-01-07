using System.ComponentModel.DataAnnotations;

namespace TwitterCopy.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}