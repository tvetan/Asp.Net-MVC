namespace StoringImages.Models
{   
    using System.ComponentModel.DataAnnotations; 
    using System.Web;
    using StoringImages.Models.DataAnnotations;

    public class UploadFileModel
    {
        [FileSize(10240)]
        [FileTypes("jpg, jpeg, png")]
        [Required(ErrorMessage="File is required")]
        public HttpPostedFileBase File { get; set; }
    }
}