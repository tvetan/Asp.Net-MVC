using System;
using System.Linq;
using TwitterCopy.Models;

namespace TwitterCopy.Data.Repositories.Contracts
{
    public interface IUsersRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetByUsername(string username, bool includeProfile = false,
            bool includeTweets = false, bool includeFollowers = false, bool includeFollowing = false);

        ApplicationUser GetById(string id, bool includeProfile = false, bool includeTweets = false,
            bool includeFollowers = false, bool includeFollowing = false);
    }
}
