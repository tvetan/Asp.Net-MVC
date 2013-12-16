using System;
using TwitterCopy.Data.Repositories.Contracts;

namespace TwitterCopy.Data
{
    public interface ITwitterCopyData : IDisposable
    {
        IUsersRepository Users { get;}

        IUserProfileRepository UserProfile { get; }
        ITweetRepository Tweets { get;}

        TwitterCopyDbContext Context { get; }

        int SaveChanges();
    }
}
