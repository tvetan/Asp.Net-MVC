using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Books.Entities;
using System.Diagnostics;

namespace Books.Data
{
    public class BooksDb : DbContext
    {
        public BooksDb()
            : base("DefaultConnection")
        {
            Database.Log = sql => Debug.Write(sql);
        }
        public DbSet<Book> Books { get; set; }
    }
}
