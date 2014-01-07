namespace TwitterCopy.Models.FeedbackReportModels
{
    using System.ComponentModel.DataAnnotations;
    using TwitterCopy.Models;

    public class FeedbackReportViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Subject should be between {2} and {1} symbols.")]
        public string Subject { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Description of problem")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "{0} is too short.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [UIHint("FeedbackReportTypeDownList")]
        public FeedbackReportType Type { get; set; }

        public string Email { get; set; }

        [UIHint("NonEditable")]
        public string Username { get; set; }

        [Display(Name = "Phone number (optional)")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is Required.")]
        public string FullName { get; set; }
    }
}