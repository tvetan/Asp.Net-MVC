using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCopy.Data;
using TwitterCopy.Models;
using Microsoft.AspNet.Identity;

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
	}
}