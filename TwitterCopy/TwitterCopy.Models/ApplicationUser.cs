using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using TwitterCopy.Models.Contracts;

namespace TwitterCopy.Models
{
    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        public ApplicationUser()
        {
            this.tweets = new HashSet<Tweet>();
            this.followers = new HashSet<ApplicationUser>();
            this.followings = new HashSet<ApplicationUser>();
        }
        public string Email { get; set; }

        public string City { get; set; }

        public string WebSite { get; set; }

        public string FullName { get; set; }

        public int? UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        private ICollection<Tweet> tweets;

        public virtual ICollection<Tweet> Tweets
        {
            get
            {
                return tweets;
            }
            set
            {
                tweets = value;
            }
        }

        private ICollection<ApplicationUser> followings;

        public virtual ICollection<ApplicationUser> Followings
        {
            get
            {
                return followings;
            }
            set
            {
                this.followings = value;
            }
        }

        private ICollection<ApplicationUser> followers;

        public virtual ICollection<ApplicationUser> Followers
        {
            get
            {
                return followers;
            }
            set
            {
                followers = value;
            }
        }

        #region IAuditInfo
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        #endregion
    }
}
