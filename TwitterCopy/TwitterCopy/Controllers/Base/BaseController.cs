using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCopy.Data;
using TwitterCopy.Models;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using TwitterCopy.Web.Common;

namespace TwitterCopy.Controllers.Base
{
    public class BaseController : Controller
    {
        public BaseController(ITwitterCopyData data)
        {
            this.Data = data;
        }

        protected ITwitterCopyData Data { get; set; }

        protected ApplicationUser GetLogInUser()
        {
            string userId = User.Identity.GetUserId();

            var user = this.Data.Users.GetById(userId, true, true, true, true);

            return user;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            // Calling BeginExecute before PrepareSystemMessages for the TempData to has values
            var result = base.BeginExecute(requestContext, callback, state);

            var systemMessages = this.PrepareSystemMessages();
            this.ViewBag.SystemMessages = systemMessages;

            return result;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (this.Request.IsAjaxRequest())
            {
                var exception = filterContext.Exception as HttpException;

                if (exception != null)
                {
                    this.Response.StatusCode = exception.GetHttpCode();
                    this.Response.StatusDescription = exception.Message;
                }
            }
            else
            {
                var controllerName = ControllerContext.RouteData.Values["Controller"].ToString();
                var actionName = ControllerContext.RouteData.Values["Action"].ToString();

                this.View("Error", new HandleErrorInfo(filterContext.Exception, controllerName, actionName)).ExecuteResult(this.ControllerContext);
            }

            filterContext.ExceptionHandled = true;
        }

        private SystemMessageCollection PrepareSystemMessages()
        {
            var messages = new SystemMessageCollection();

            if (this.TempData.ContainsKey("InfoMessage"))
            {
                messages.Add(this.TempData["InfoMessage"].ToString(), SystemMessageType.Success, 1000);
            }

            if (this.TempData.ContainsKey("DangerMessage"))
            {
                messages.Add(this.TempData["DangerMessage"].ToString(), SystemMessageType.Error, 1000);
            }

            return messages;
        }
	}
}