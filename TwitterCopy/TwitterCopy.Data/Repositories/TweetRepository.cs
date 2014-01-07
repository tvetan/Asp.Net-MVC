namespace TwitterCopy.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TwitterCopy.Data.Repositories.Base;
    using TwitterCopy.Data.Repositories.Contracts;
    using TwitterCopy.Models;

    public class TweetRepository : GenericRepository<Tweet>, ITweetRepository
    {
        public TweetRepository(ITwitterCopyDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Tweet> GetByUser(ApplicationUser user)
        {
            return user.Tweets.OrderByDescending(r => r.CreatedOn);
        }

        public void AddToUser(Tweet tweet, ApplicationUser user)
        {
            user.Tweets.Add(tweet);           
        }
    }
}