namespace TwitterCopy.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    using TwitterCopy.Controllers.Base;
    using TwitterCopy.Data;
    using TwitterCopy.Models;
    using System.Net;
    using System.Web;
    

    public class TweetController : BaseController
    {
        public TweetController(ITwitterCopyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(CreateTweetViewModel model)
        {
           if (ModelState.IsValid)
            {
                Tweet tweet = new Tweet();
                tweet.Status = model.Status;
                tweet.CreatedOn = DateTime.Now;
                tweet.AuthorId = User.Identity.GetUserId();

                this.Data.Context.Tweets.Add(tweet);
                this.Data.SaveChanges();
            }

           return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var tweet = this.Data.Tweets.GetById(id);
            var user = this.GetLogInUser();
            if (tweet != null && user.UserName == tweet.Author.UserName)
            {
                this.Data.Tweets.Delete(tweet);
                this.Data.SaveChanges();

                this.TempData["InfoMessage"] = "Tweet was deleted succefully";
            }
            else
            {
                this.TempData["DangerMessage"] = "There was a problem deleting the tweet ";
            }

            return RedirectToAction("Index", "User", new { username = user.UserName });
        }

        [ChildActionOnly]
        [HttpGet]
        [ActionName("ListTweets")]
        public ActionResult ListTweets(string username, bool getFallowingsTweets = false)
        {
            var user = this.Data.Users.GetByUsername(username);

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "User is not found");
            }

            var tweets = this.Data.Tweets.GetByUser(user);

            if (getFallowingsTweets)
	        {
                var fallowingsTweets = user.Followings.AsQueryable().SelectMany(x => x.Tweets);
                tweets = fallowingsTweets.Union(tweets);
	        }

            tweets = tweets.OrderByDescending(x => x.CreatedOn);

            return this.PartialView("_TweetsList", tweets.ToList());
        }
    }
}