

using Microsoft.AspNet.Identity.EntityFramework;
using StoringImages.Models;
using System.Data.Entity;
namespace StoringImages.Data
{
    public class StoringImagesDbContext : IdentityDbContext<ApplicationUser>, IStoringImagesDbContext
    {
        public StoringImagesDbContext()
            : base("DefaultConnection")
        {
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public virtual DbSet<Document> Documents { get; set; }
    }
}
