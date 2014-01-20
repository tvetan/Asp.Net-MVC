namespace TwitterCopy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using TwitterCopy.Controllers.Base;
    using TwitterCopy.Data;
    using TwitterCopy.Models;

    public class UserController : BaseController
    {
        private const string FollowersHeader = "Followers";
        private const string FollowingHeader = "Followings";
        private readonly Random random = new Random();

        public UserController(ITwitterCopyData data) : base(data)
        {
        }

        public ActionResult Index(string username)
        {
            ApplicationUser user = this.GetUserByUserNameOrLogInUser(username);

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Username was not found");
            }

            return this.View(user);
        }

        public ActionResult RandomUsers()
        {
            var logInUser = this.GetLogInUser();
            var filteredUsers = this.Data.Users.GetUsersWithoutCurrentUsersAndHisFollowings(logInUser);

            IEnumerable<ApplicationUser> randomUsers = this.GetRandomUsers(filteredUsers.ToList());

            return this.PartialView("_ListRandomUsers", randomUsers);
        }

        public ActionResult Suggestions()
        {
            var logInUser = this.GetLogInUser();
            var users = this.Data.Users
                .GetUsersWithoutCurrentUsersAndHisFollowings(logInUser).OrderBy(user => user.Followers.Count);

            return this.View(users);
        }

        public ActionResult UploadProfilePicture(string username)
        {
            var user = this.Data.Users.GetByUsername(username);

            return File(user.ProfilePicture.Content, user.ProfilePicture.Type);
        }

        [ChildActionOnly]
        public ActionResult ListFollowings()
        {
            var logInUser = this.GetLogInUser();
            var users = logInUser.Followings;

            ViewBag.headerText = FollowingHeader;

            return this.PartialView("_UsersListPartial", users.ToList());
        }

        [ChildActionOnly]
        public ActionResult ListFollowers()
        {
            var logInUser = this.GetLogInUser();
            var users = logInUser.Followers;
            ViewBag.headerText = FollowersHeader;

            return this.PartialView("_UsersListPartial", users.ToList());
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
    }
}