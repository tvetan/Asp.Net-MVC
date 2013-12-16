using System;
using System.Linq;
using System.Web.Mvc;
using TwitterCopy.Controllers.Base;
using TwitterCopy.Data;
using TwitterCopy.Models;

namespace TwitterCopy.Controllers
{
    public class UserController : BaseController
    {
        private const string FollowersHeader = "Followers";
        private const string FollowingHeader = "Followings";
        public UserController(ITwitterCopyData data)
            : base(data)
        {
        }

        // GET: /User/
        public ActionResult Index(string username)
        {
            ApplicationUser user = GetUserByUserNameOrLogInUser(username);

            if (user == null)
            {
                throw new ArgumentNullException("user", "There is no user found");
            }

            return View(user);
        }
  
        private ApplicationUser GetUserByUserNameOrLogInUser(string username)
        {
            ApplicationUser user;
            if (username != null)
            {
                user = this.Data.Users.GetByUsername(username);
            }
            else
            {
                user = this.GetLogInUser();
            }

            return user;
        }

        [ChildActionOnly]
        public ActionResult RandomUsers()
        {
            var logInUser = this.GetLogInUser();
            var users = this.Data.Users.All()
                .Where(user => user.Id != logInUser.Id)
                // .Where(user => !user.Followers.Contains(logInUser))
                .Take(3);

            return PartialView("_ListRandomUsers", users.ToList());
        }

        [ChildActionOnly]
        public ActionResult ListFollowings()
        {
            var logInUser = this.GetLogInUser();
            var users = logInUser.Followings;
            ViewBag.headerText = FollowingHeader;

            return PartialView("_UsersListPartial", users.ToList());
        }

        [ChildActionOnly]
        public ActionResult ListFollowers()
        {
            var logInUser = this.GetLogInUser();
            var users = logInUser.Followers;
            ViewBag.headerText = FollowersHeader;

            return PartialView("_UsersListPartial", users.ToList());
        }
    }
}