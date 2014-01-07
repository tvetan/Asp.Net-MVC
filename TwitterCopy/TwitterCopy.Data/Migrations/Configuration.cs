namespace TwitterCopy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    using TwitterCopy.Models;

    public class Configuration : DbMigrationsConfiguration<TwitterCopy.Data.TwitterCopyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TwitterCopy.Data.TwitterCopyDbContext context)
        {
            if (context.Users.Count() > 0)
            {
                return;
            }

            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedAdministrators(context);
            this.SeedLanguages(context);
            this.SeedTimeZones(context);
            this.SeedCountries(context);  
        }

        protected void SeedRoles(TwitterCopyDbContext context)
        {
            foreach (var entity in context.Roles)
            {
                context.Roles.Remove(entity);
            }

            context.Roles.AddOrUpdate(new IdentityRole("Administrator"));
        }

        private void SeedLanguages(TwitterCopyDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.Languages.Add(new Language()
                {
                    Code = i.ToString(),
                    Name = "language" + i
                });
            }
        }

        private void SeedCountries(TwitterCopyDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.Countries.Add(new Country()
                {
                    Code = i.ToString(),
                    Name = "Country" + i
                });
            }
        }

        private void SeedTimeZones(TwitterCopyDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.TimeZones.Add(new TwitterCopy.Models.TimeZone()
                {
                    Code = i.ToString(),
                    Name = "TimeZone" + i
                });
            }
        }

        private void SeedUsers(TwitterCopyDbContext context)
        {
            DateTime today = DateTime.Today;
            if (!context.Users.Any(u => u.UserName == "founder"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                for (int i = 0; i < 20; i++)
                {
                    ApplicationUser user = new ApplicationUser() { CreatedOn = today };
                    user.Email = "tvetan@gbg.bg" + i;
                    user.UserName = "tvetan" + i;

                    manager.Create(user, "tvetan" + i);
                }
            }
        }

        private void SeedAdministrators(TwitterCopyDbContext context)
        {
            var user = context.Users.FirstOrDefault();
            var role = context.Roles.FirstOrDefault(r => r.Name == "Administrator");

            if (user == null || role == null)
            {
                return;
            }

            user.Roles.Add(new IdentityUserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });
        }
    }
}
