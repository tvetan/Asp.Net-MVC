using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TwitterCopy.Models
{
    public class UserProfile
    {
       // [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        //public string UserId { get; set; }

        //[Required]
        //public ApplicationUser ApplicationUser { get; set; }
     
        public string Email { get; set; }

        public string Name { get; set; }

        public string WebsiteUrl { get; set; }

        public string Bio { get; set; }
    }
}
