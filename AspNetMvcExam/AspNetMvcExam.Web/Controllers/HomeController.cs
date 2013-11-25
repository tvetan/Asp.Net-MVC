using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AspNetMvcExam.Models;
using AspNetMvcExam.Web.Models;

namespace AspNetMvcExam.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.HttpContext.Cache["Tickets"] == null)
            {
                var tickets = data.Tickets
                                  .All()
                                  .OrderByDescending(x => x.Comments.Count)
                                  .Take(6);

                this.HttpContext.Cache.Add("Tickets", tickets.ToList(),
                    null, DateTime.Now.AddHours(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            return View(this.HttpContext.Cache["Tickets"]);
        }

        public ActionResult Details(int id)
        {
            var ticket = data.Tickets.GetById(id);

            return View(ticket);
        }

        // [Authorize]
        //  [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PostComment(CommentViewModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                data.Comments.Add(new Comment()
                {
                    UserId = userId,
                    Content = commentModel.Content,
                    TicketId = commentModel.TicketId,
                });

                this.data.SaveChanges();

                var viewModel = new CommentViewModel { UserName = username, Content = commentModel.Content };
                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
    }
}