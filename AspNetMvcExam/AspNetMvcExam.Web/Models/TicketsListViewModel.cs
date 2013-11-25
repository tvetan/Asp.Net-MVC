using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMvcExam.Models;

namespace AspNetMvcExam.Web.Models
{
    public class TicketsListViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public Priority Priority { get; set; }

        public string CategoryName { get; set; }
    }
}