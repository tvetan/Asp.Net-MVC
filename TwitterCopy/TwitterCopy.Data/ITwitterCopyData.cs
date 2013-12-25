using System;
using TwitterCopy.Data.Repositories;
using TwitterCopy.Data.Repositories.Contracts;
using TwitterCopy.Models;

namespace TwitterCopy.Data
{
    public interface ITwitterCopyData : IDisposable
    {
        IUsersRepository Users { get;}

       // IUserProfileRepository UserProfile { get; }
        ITweetRepository Tweets { get;}

        TwitterCopyDbContext Context { get; }

        IRepository<Language> Languages{ get; }

        IRepository<TwitterCopy.Models.TimeZone> TimeZones { get; }

        IRepository<Country> Countries{ get; }

        int SaveChanges();
    }
}
