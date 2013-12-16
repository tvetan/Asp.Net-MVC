using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvcExam.Web.Models
{
    public class CommentViewModel
    {
        public string UserName { get; set; }
        public string Content { get; set; }

        public int TicketId { get; set; }
         
    }
}