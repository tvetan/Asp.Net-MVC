namespace AspNetMvcExam.Data.Migrations
{
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using AspNetMvcExam.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class DatabaseInitializer : DbMigrationsConfiguration<AspNetMvcExam.Data.ApplicationDbContext>
    {
        public DatabaseInitializer()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AspNetMvcExam.Data.ApplicationDbContext context)
        {
            if (context.Categories.Count() > 0)
            {
                return;
            }

            Random rand = new Random();

            Category category = new Category { Name = "Very Bad" };
            ApplicationUser user = new ApplicationUser() { UserName = "TestUser11", Points = 10 };

            for (int i = 0; i < 10; i++)
            {
                Ticket ticket = new Ticket();

                ticket.Url = "img/ticket.png";
                ticket.Category = category;
                ticket.Title = "very big b big big";
                ticket.Priority = Priority.Medium;
                ticket.Author = user;
                ticket.Description = "this is the description";
                var commentsCount = rand.Next(0, 10);
                for (int j = 0; j < commentsCount; j++)
                {
                    ticket.Comments.Add(new Comment { Content = "There was a bug in the project.", User = user });
                }

                context.Tickets.AddOrUpdate(ticket);
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            base.Seed(context);
        }
    }
}
