namespace TwitterCopy.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using TwitterCopy.Models.DataAnnotations;

    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Location { get; set; }

        public string Website { get; set; }

        [FileSize(240240)]
        [FileTypes("jpg, jpeg, png")]
        //[Required(ErrorMessage = "File is required")]
        public HttpPostedFileBase ProfilePicture { get; set; }

        [UIHint("MultilineText")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} length Should be between {2} and {1} symbols long")]
        public string Bio { get; set; }

        public int? ProfilePictureId { get; set; }
    }
}