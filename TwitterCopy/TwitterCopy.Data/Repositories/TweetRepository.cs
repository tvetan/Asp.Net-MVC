namespace TwitterCopy.Data.Repositories
{
    using System.Data.Entity;
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

        public IQueryable<Tweet> GetByUser(ApplicationUser user)
        {
            return this.Context.Tweets.Where(x => x.AuthorId == user.Id)
                .Include(x => x.Author);
        }

        public void AddToUser(Tweet tweet, ApplicationUser user)
        {
            user.Tweets.Add(tweet);           
        }
    }
}