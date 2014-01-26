namespace TwitterCopy.Tests.Common.DataFakes
{
    using System.Data.Entity.Migrations;

    internal sealed class DatabaseConfiguration : DbMigrationsConfiguration<FakeTwitterCopyDbContext>
    {
        public DatabaseConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FakeTwitterCopyDbContext context)
        {
            //context.ClearDatabase();
           // this.SeedContests(context);
        }
    }
}