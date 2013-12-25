using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCopy.Tests.Common.DataFakes
{
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
