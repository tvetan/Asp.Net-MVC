using System;
using System.Collections.Generic;
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
        private readonly Random random = new Random();

        public UserController(ITwitterCopyData data) : base(data)
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

        public ActionResult RandomUsers()
        {
            var logInUser = this.GetLogInUser();
            var followingsIds = logInUser.Followings.Select(user => user.Id);

            var filteredUsers = this.Data.Users.All()
                            .Where(user => user.Id != logInUser.Id && !followingsIds.Contains(user.Id));

            IEnumerable<ApplicationUser> randomUsers = this.GetRandomUsers(filteredUsers.ToList());

            return PartialView("_ListRandomUsers", randomUsers);
        }

        private IEnumerable<ApplicationUser> GetRandomUsers(IList<ApplicationUser> users)
        {
            if (users.Count() <= 3)
            {
                return users;
            }

            IList<ApplicationUser> randomUsers = new List<ApplicationUser>();
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = this.random.Next(users.Count());
                randomUsers.Add(users.ElementAt(randomNumber));
                users.RemoveAt(randomNumber);
            }

            return randomUsers;
        }

        public ActionResult Suggestions()
        {
            return View();
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