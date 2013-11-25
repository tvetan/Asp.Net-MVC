using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AspNetMvcExam.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }
       
        public virtual ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        [Required]
        [ShouldNotContainBugAttribute(ErrorMessage="Sorry the word bug should not be used in the title'")]
        public string Title { get; set; }

        [Required]
        public Priority Priority { get; set; }

        private ICollection<Comment> comments;

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public Ticket()
        {
            this.comments = new HashSet<Comment>();
        }
    }
}
