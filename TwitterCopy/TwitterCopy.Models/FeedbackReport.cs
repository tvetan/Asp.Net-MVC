namespace TwitterCopy.Models
{
    using System.ComponentModel.DataAnnotations;

    using TwitterCopy.Models.Contracts;

    public class FeedbackReport : DeletebleEntity
    {
        [Key]
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public FeedbackReportType Type { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public bool IsFixed { get; set; }
    }
}