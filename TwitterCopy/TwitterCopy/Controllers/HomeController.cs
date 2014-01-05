using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TwitterCopy.Data;
using TwitterCopy.Controllers.Base;
using TwitterCopy.Models;

namespace TwitterCopy.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ITwitterCopyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var user = GetLogInUser();
            return View(user);
        }

        public ActionResult Follow(string id)
        {
            var currentUser = this.GetLogInUser();
            var followingUser = this.Data.Users.GetById(id);

            currentUser.Followings.Add(followingUser);
            followingUser.Followers.Add(currentUser);
            this.Data.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult UnFollow(string id)
        {
            var currentUser = this.GetLogInUser();
            var followingUser = this.Data.Users.GetById(id);

            currentUser.Followers.Remove(followingUser);
            followingUser.Followers.Remove(currentUser);
            this.Data.SaveChanges();

            return RedirectToAction("index");
        }
        
        public ActionResult Mentions()
        {
            return View();
        }
        
        [ActionName("followings")]
        public ActionResult Followings()
        {
            var userId = this.User.Identity.GetUserId();
            var userWithFollowings = this.Data.Users.GetById(userId, false, false, false, true);
           
            return this.View(userWithFollowings);
        }

        [ActionName("followers")]
        public ActionResult Follower()
        {
            var userId = this.User.Identity.GetUserId();
            var userWithFollowers = this.Data.Users.GetById(userId, false, false, true, false);

            return this.View(userWithFollowers);
        }
    }
}