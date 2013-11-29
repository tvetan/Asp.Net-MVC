using System;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Books.Data
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection")
        {
        }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}