using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayApplication.Models
{
    public class Answer
    {
        // demo commit
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
