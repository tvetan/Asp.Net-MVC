using System;
using System.Collections.Generic;
using System.Linq;
using TwitterCopy.Models;

namespace TwitterCopy.Data.Repositories.Contracts
{
    public interface ITweetRepository : IRepository<Tweet>
    {
        Tweet GetById(int id);
        IEnumerable<Tweet> GetByUser(ApplicationUser user);

        void AddToUser(Tweet tweet, ApplicationUser user);
    }
}
