namespace TwitterCopy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TwitterCopy.Models.Contracts;
    using System.ComponentModel;

    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        private ICollection<Tweet> tweets;

        private ICollection<ApplicationUser> followers;

        private ICollection<ApplicationUser> followings;

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

        public virtual ICollection<Tweet> Tweets
        {
            get
            {
                return this.tweets;
            }

            set
            {
                this.tweets = value;
            }
        }

        public virtual ICollection<ApplicationUser> Followings
        {
            get
            {
                return this.followings;
            }

            set
            {
                this.followings = value;
            }
        }

        public virtual ICollection<ApplicationUser> Followers
        {
            get
            {
                return this.followers;
            }

            set
            {
                this.followers = value;
            }
        }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int? LanguageId { get; set; }

        public virtual Language Language { get; set; }

        //[DefaultValue(1)]
        public int? TimeZoneId { get; set; }

        public virtual TimeZone TimeZone { get; set; }

        #region IAuditInfo
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        #endregion
    }
}
