namespace TwitterCopy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public string Type { get; set; }
    }
}