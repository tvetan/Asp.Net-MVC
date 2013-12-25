namespace TwitterCopy.Tests.Common.WebStubs
{
    using System.Web;

    public class StubHttpRequestForRouting : HttpRequestBase
    {
        private string applicationPath;

        private string requestUrl;

        public StubHttpRequestForRouting(string applicationPath, string requestUrl)
        {
            this.applicationPath = applicationPath;
            this.requestUrl = requestUrl;
        }

        public override string ApplicationPath
        {
            get
            {
                return this.applicationPath;
            }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return this.requestUrl;
            }
        }

        public override string PathInfo
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
