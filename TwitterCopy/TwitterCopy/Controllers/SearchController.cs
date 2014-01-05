using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCopy.Controllers.Base;
using TwitterCopy.Data;

namespace TwitterCopy.Controllers
{
    public class SearchController : BaseController
    {
        public SearchController(ITwitterCopyData data)
            : base(data)
        {
        }

        public ActionResult SearchHome()
        {
            return this.View();
        }


        [HttpGet]
        public ActionResult Result(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return RedirectToAction("SearchHome");
            }

            var words = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var filteredUsers = this.Data.Users.All()
                .Where(user => words.Any(word => user.UserName.Contains(word)));

            ViewBag.searchQuery = search;

            return this.View(filteredUsers);
        }
	}
}