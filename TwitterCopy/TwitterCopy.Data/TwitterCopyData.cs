namespace TwitterCopy.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TwitterCopy.Data.Repositories;
    using TwitterCopy.Data.Repositories.Base;
    using TwitterCopy.Data.Repositories.Contracts;
    using TwitterCopy.Models;
    using TwitterCopy.Models.Contracts;

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

        public IRepository<Language> Languages
        {
            get
            {
                return this.GetRepository<Language>();
            }
        }

        public IRepository<TwitterCopy.Models.TimeZone> TimeZones
        {
            get
            {
                return this.GetRepository<TwitterCopy.Models.TimeZone>();
            }
        }

        public IRepository<Country> Countries
        {
            get
            {
                return this.GetRepository<Country>();
            }
        }

        public IRepository<Document> Documents
        {
            get
            {
                return this.GetRepository<Document>();
            }
        }

        public IUsersRepository Users
        {
            get
            {
                return (UsersRepository)this.GetRepository<ApplicationUser>();
            }
        }

        public ITweetRepository Tweets
        {
            get
            {
                return (TweetRepository)this.GetRepository<Tweet>();
            }
        }

        public IDeletableEntityRepository<FeedbackReport> FeedbackReports
        {
            get
            {
                return this.GetDeletableEntityRepository<FeedbackReport>();
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

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}