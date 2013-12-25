using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterCopy.Data.Repositories.Base;
using TwitterCopy.Data.Repositories.Contracts;
using TwitterCopy.Models;

namespace TwitterCopy.Data.Repositories
{
    class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(TwitterCopyDbContext context)
            : base(context)
        {
        }

        public UserProfile GetById(string id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }
    }
}
