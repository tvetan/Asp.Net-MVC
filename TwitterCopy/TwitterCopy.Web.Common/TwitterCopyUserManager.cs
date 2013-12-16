using System;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace TwitterCopy.Web.Common
{
    public class TwitterCopyUserManager<T> : UserManager<T> where T : IUser
    {
        public TwitterCopyUserManager(IUserStore<T> userStore)
            : base(userStore)
        {
            // changing the default user validator so that usernames can contain
            // not only alphanumeric characters
            this.UserValidator = new UserValidator<T>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }
    }
}
