using System;
using System.Data.Entity;
namespace StoringImages.Data
{
    public interface IStoringImagesDbContext
    {
        DbSet<StoringImages.Models.Document> Documents { get; set; }

        int SaveChanges();
    }
}
