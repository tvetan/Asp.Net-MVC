using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterCopy.Models
{
    public class CreateTweetViewModel
    {
        [Required]
        [StringLength(140, ErrorMessage="Status is too long")]
        public string Status { get; set; }
    }
}