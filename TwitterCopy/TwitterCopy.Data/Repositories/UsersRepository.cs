using System;
using System.Linq;
using TwitterCopy.Data.Repositories.Base;
using TwitterCopy.Data.Repositories.Contracts;
using TwitterCopy.Models;
using System.Data.Entity;

namespace TwitterCopy.Data.Repositories
{
    public class UsersRepository : GenericRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(TwitterCopyDbContext context)
            : base(context)
        {
        }

        public ApplicationUser GetByUsername(string username, bool includeProfile = false,
            bool includeTweets = false, bool includeFollowers = false, bool includeFollowing = false)
        {
            var query = BuildUserQuery(includeProfile, includeTweets, includeFollowers, includeFollowing);

            return query.SingleOrDefault(u => u.UserName == username);
        }


        public ApplicationUser GetById(string id, bool includeProfile = false,
            bool includeTweets = false, bool includeFollowers = false, bool includeFollowing = false)
        {
            var query = BuildUserQuery(includeProfile, includeTweets, includeFollowers, includeFollowing);

            return query.SingleOrDefault(u => u.Id == id);
        }
  
        private IQueryable<ApplicationUser> BuildUserQuery(bool includeProfile, bool includeTweets, bool includeFollowers, bool includeFollowing)
        {
            var query = this.DbSet.AsQueryable();
            if (includeProfile)
            {
                query = this.DbSet.Include(u => u.UserProfile);
            }

            if (includeTweets)
            {
                query = this.DbSet.Include(u => u.Tweets);
            }

            if (includeFollowers)
            {
                query = this.DbSet.Include(u => u.Followers);
            }

            if (includeFollowing)
            {
                query = this.DbSet.Include(u => u.Followings);
            }

            return query;
        }  
    }
}
