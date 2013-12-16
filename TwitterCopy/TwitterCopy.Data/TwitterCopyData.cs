using System;
using System.Collections.Generic;
using System.Linq;
using TwitterCopy.Data.Repositories;
using TwitterCopy.Data.Repositories.Base;
using TwitterCopy.Data.Repositories.Contracts;
using TwitterCopy.Models;

namespace TwitterCopy.Data
{
    public class TwitterCopyData : ITwitterCopyData
    {

        private readonly TwitterCopyDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public TwitterCopyData()
            : this(new TwitterCopyDbContext())
        {
        }

        public TwitterCopyData(TwitterCopyDbContext context)
        {
            this.context = context;
        }

        public IUsersRepository Users
        {
            get
            {
                return (UsersRepository)this.GetRepository<ApplicationUser>();
            }
        }

        public IUserProfileRepository UserProfile
        {
            get
            {
                return (UserProfileRepository)this.GetRepository<UserProfile>();
            }
        }

        public ITweetRepository Tweets
        {
            get
            {
                return (TweetRepository)this.GetRepository<Tweet>();
            }
        }

        public TwitterCopyDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                if (typeof(T).IsAssignableFrom(typeof(ApplicationUser)))
                {
                    type = typeof(UsersRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(Tweet)))
                {
                    type = typeof(TweetRepository);
                }

                if (typeof(T).IsAssignableFrom(typeof(UserProfile)))
                {
                    type = typeof(UserProfileRepository);
                }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
