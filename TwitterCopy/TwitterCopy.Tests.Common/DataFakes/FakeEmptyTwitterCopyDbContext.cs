namespace TwitterCopy.Tests.Common.DataFakes
{
    using TwitterCopy.Data;

    class FakeEmptyTwitterCopyDbContext : TwitterCopyDbContext
    {
        public FakeEmptyTwitterCopyDbContext()
            : base("FakeEmptyTwitterCopyDbContext")
        {
        }
    }
}
