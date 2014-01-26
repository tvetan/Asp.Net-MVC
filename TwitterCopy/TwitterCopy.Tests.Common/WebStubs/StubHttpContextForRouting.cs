﻿namespace TwitterCopy.Tests.Common.WebStubs
{
    using System.Web;

    public class StubHttpContextForRouting : HttpContextBase
    {

        private readonly StubHttpRequestForRouting request;

        private readonly StubHttpResponseForRouting response;

        public StubHttpContextForRouting(string applicationPath = "/", string requestUrl = "/")
        {
            this.request = new StubHttpRequestForRouting(applicationPath, requestUrl);
            this.response = new StubHttpResponseForRouting();
        }

        public override HttpRequestBase Request
        {
            get
            {
                return this.request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return this.response;
            }
        }
    }
}