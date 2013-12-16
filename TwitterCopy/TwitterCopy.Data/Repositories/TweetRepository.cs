using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TwitterCopy.Data.Repositories.Base;
using TwitterCopy.Data.Repositories.Contracts;
using TwitterCopy.Models;

namespace TwitterCopy.Data.Repositories
{
    public class TweetRepository : GenericRepository<Tweet>, ITweetRepository
    {
        public TweetRepository(TwitterCopyDbContext context)
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

        public Tweet GetById(int id)
        {
            return this.Find(r => r.Id == id);
        }
    }
}
