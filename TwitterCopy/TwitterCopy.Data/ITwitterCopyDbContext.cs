namespace TwitterCopy.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TwitterCopy.Models;

    public interface ITwitterCopyDbContext
    {
        //IDbSet<UserProfile> UserProfiles { get; set; }

        IDbSet<Tweet> Tweets { get; set; }

        IDbSet<Language> Languages { get; set; }

        IDbSet<TwitterCopy.Models.TimeZone> TimeZones { get; set; }

        IDbSet<Country> Countries { get; set; }

        DbContext DbContext { get; }

        IDbSet<T> Set<T>() where T : class;   

        int SaveChanges();

        void ApplyAuditInfoRules();
    }
}