using System;
using System.Linq;
using TwitterCopy.Models;

namespace TwitterCopy.Common.Extensions
{
    public static class ApplicationUserExtenstions
    {
        public static bool DoesUserContainFollower(this ApplicationUser user, string followerId)
        {
            var followers = user.Followers;

            foreach (var follower in followers)
            {
                if (follower.Id == followerId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
