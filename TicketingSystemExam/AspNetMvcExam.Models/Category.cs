using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AspNetMvcExam.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength=5, ErrorMessage="The category name must be between 5 and 50")]
        public string Name { get; set; }

        private ICollection<Ticket> tickets;
        public virtual ICollection<Ticket> Tickets
        {
            get
            {
                return this.tickets;
            }

            set
            {
                this.tickets = value;
            }
        }

        public Category()
        {
            this.tickets = new HashSet<Ticket>();
        }
    }
}
