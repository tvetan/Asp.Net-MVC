namespace TwitterCopy.Tests.Common.DataFakes
{
    using TwitterCopy.Data;

    public class FakeTwitterCopyDbContext : TwitterCopyDbContext
    {
        public FakeTwitterCopyDbContext()
            : base("FakeTwitterCopyDbContext")
        {
        }
    }
}