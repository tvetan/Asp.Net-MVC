namespace TwitterCopy.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using TwitterCopy.Controllers.Base;
    using TwitterCopy.Data;
    using TwitterCopy.Models;

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

        [ChildActionOnly]
        [HttpGet]
        [ActionName("ListTweets")]
        public ActionResult ListTweets()
        {
            var user = GetLogInUser();
            var tweets = this.Data.Tweets.GetByUser(user);

            return this.PartialView("_TweetsList", tweets);
        }
    }
}