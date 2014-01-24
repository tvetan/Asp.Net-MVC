namespace TwitterCopy.Data.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TwitterCopy.Models;

    public interface ITweetRepository : IRepository<Tweet>
    {
        IQueryable<Tweet> GetByUser(ApplicationUser user);

        void AddToUser(Tweet tweet, ApplicationUser user);
    }
}