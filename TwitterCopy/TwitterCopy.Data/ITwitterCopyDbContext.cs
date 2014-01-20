namespace TwitterCopy.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using TwitterCopy.Models;

    public interface ITwitterCopyDbContext : IDisposable
    {
        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<FeedbackReport> FeedbackReports { get; set; }

        IDbSet<Language> Languages { get; set; }

        IDbSet<TwitterCopy.Models.TimeZone> TimeZones { get; set; }

        IDbSet<Document> Documents { get; set; }

        IDbSet<Country> Countries { get; set; }

        DbContext DbContext { get; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;

        int SaveChanges();  
    }
}