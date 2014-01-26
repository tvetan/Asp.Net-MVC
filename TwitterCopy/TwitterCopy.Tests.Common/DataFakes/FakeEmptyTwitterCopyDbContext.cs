namespace TwitterCopy.Tests.Common.DataFakes
{
    using TwitterCopy.Data;

    public class FakeEmptyTwitterCopyDbContext : TwitterCopyDbContext
    {
        public FakeEmptyTwitterCopyDbContext()
            : base("FakeEmptyTwitterCopyDbContext")
        {
        }
    }
}