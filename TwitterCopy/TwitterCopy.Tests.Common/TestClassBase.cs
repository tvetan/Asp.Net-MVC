namespace TwitterCopy.Tests.Common
{
    using System.Data.Entity;
    using TwitterCopy.Data;
    using TwitterCopy.Tests.Common.DataFakes;

    public abstract class TestClassBase
    {
        protected TestClassBase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FakeTwitterCopyDbContext, DatabaseConfiguration>());
            this.TwitterCopyData = new TwitterCopyData(new FakeTwitterCopyDbContext());
            this.InitializeEmptyTwitterCopyData();
        }

        protected ITwitterCopyData EmptyTwitterCopyDAta { get; set; }

        protected ITwitterCopyData TwitterCopyData { get; set; }

        protected void InitializeEmptyTwitterCopyData()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<FakeEmptyTwitterCopyDbContext>());
            var fakeEmptyTwitterCopyDbContext = new FakeEmptyTwitterCopyDbContext();
            fakeEmptyTwitterCopyDbContext.Database.Initialize(true);
            this.EmptyTwitterCopyDAta = new TwitterCopyData(fakeEmptyTwitterCopyDbContext);
        }
    }
}