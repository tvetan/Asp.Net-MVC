using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayApplication.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250,ErrorMessage="The question for the product is too long")]
        [MinLength(5, ErrorMessage="The minimum length is 5 simbols")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
    }
}
