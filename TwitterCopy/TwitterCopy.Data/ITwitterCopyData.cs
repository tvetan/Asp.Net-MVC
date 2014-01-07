namespace TwitterCopy.Data
{
    using System;

    using TwitterCopy.Data.Repositories;
    using TwitterCopy.Data.Repositories.Contracts;
    using TwitterCopy.Models;

    public interface ITwitterCopyData : IDisposable
    {
        IUsersRepository Users { get; }

        ITweetRepository Tweets { get; }

        TwitterCopyDbContext Context { get; }

        IRepository<Language> Languages { get; }

        IRepository<TwitterCopy.Models.TimeZone> TimeZones { get; }

        IRepository<Country> Countries { get; }

        IDeletableEntityRepository<FeedbackReport> FeedbackReports { get; }

        int SaveChanges();
    }
}