using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCopy.Controllers.Base;
using TwitterCopy.Data;
using TwitterCopy.Models;
using Microsoft.AspNet.Identity;

namespace TwitterCopy.Controllers
{
    public class TweetController : BaseController
    {
        public TweetController(ITwitterCopyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index");
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

           return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        [HttpGet]
        [ActionName("ListTweets")]
        public ActionResult ListTweets()
        {
            var user = GetLogInUser();
            var tweets = this.Data.Tweets.GetByUser(user);

            return PartialView("_TweetsList", tweets);
        }
    }
}