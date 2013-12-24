using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using TwitterCopy.Models;
using TwitterCopy.Models.Contracts;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace TwitterCopy.Data
{
    public class TwitterCopyDbContext : IdentityDbContext<ApplicationUser>, ITwitterCopyDbContext
    {
        public TwitterCopyDbContext() : this("TwitterCopyConnection")
        {
        }

        public TwitterCopyDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<Language> Languages { get; set; }

        public IDbSet<TwitterCopy.Models.TimeZone> TimeZones { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                        .HasMany(u => u.Followers)
                        .WithMany(u => u.Followings)
                        .Map(map =>
                        {
                            map.MapLeftKey("FollowingId");
                            map.MapRightKey("FollowerId");
                            map.ToTable("Follow");
                        });

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Tweets);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // this.ApplyAuditInfoRules();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            return base.SaveChanges();
        }

        public void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                         e =>
                             e.Entity is IAuditInfo && ((e.State == EntityState.Added) ||
                                                        (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.Today;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Today;
                }
            }
        }
        //public System.Data.Entity.DbSet<TwitterCopy.Models.SettingsViewModel> SettingsViewModels { get; set; }
    }
}