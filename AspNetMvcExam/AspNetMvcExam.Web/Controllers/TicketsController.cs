using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMvcExam.Models;
using AspNetMvcExam.Data;
using Microsoft.AspNet.Identity;
using AspNetMvcExam.Web.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace AspNetMvcExam.Web.Controllers
{
    [Authorize]
    public class TicketsController : BaseController
    {
        // GET: /Tickets/
        public ActionResult Index()
        {
            var tickets = data.Tickets
                              .All()
                              .Include(t => t.Author)
                              .Include(t => t.Category)
                              .Select(x => new TicketsListViewModel()
                                     {
                                         Author = x.Author.UserName,
                                         Priority = x.Priority,
                                         Title = x.Title,
                                         Id = x.Id,
                                         CategoryName = x.Category.Name
                                     });
            return View(tickets.ToList());
        }

        // GET: /Tickets/Details/5

        // GET: /Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(data.Users.All(), "Id", "UserName", User.Identity.GetUserId());
            ViewBag.CategoryId = new SelectList(data.Categories.All(), "Id", "Name");
            return View();
        }

        // POST: /Tickets/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.AuthorId = User.Identity.GetUserId();
                var user = data.Users.GetById(User.Identity.GetUserId());
                user.Points++;
                data.Users.Update(user);
                data.Tickets.Add(ticket);
                data.SaveChanges();
                return RedirectToAction("Index");
            }

            var priorities = from Priority s in Enum.GetValues(typeof(Priority))
                             select new { ID = s, Name = s.ToString() };
            ViewBag.Priority = new SelectList(priorities, "ID", "Name");

            //ViewData["taskStatus"] = new SelectList(statuses, "ID", "Name", task.Status);
            ViewBag.CategoryId = new SelectList(data.Categories.All(), "Id", "Name", ticket.CategoryId);
            return View(ticket);
        }

        public JsonResult GetCateogryData()
        {
            var result = this.data.Categories
                             .All()
                             .Select(x => new
                             {
                                 CategoryName = x.Name,
                                 Id = x.Id
                             });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(int? category)
        {
            //var categoryModel = data.Categories.GetById(category);
            if (category != null)
            {
                return View(data.Tickets
                                .All()
                                .Where(x => x.CategoryId == category)
                                .Select(x => new TicketsListViewModel()
                                       {
                                           CategoryName = x.Category.Name,
                                           Author = x.Author.UserName,
                                           Priority = x.Priority,
                                           Title = x.Title
                                       }));
            }
            else
            {
                return View(data.Tickets
                                .All()
                                .Select(x => new TicketsListViewModel()
                                       {
                                           CategoryName = x.Category.Name,
                                           Author = x.Author.UserName,
                                           Priority = x.Priority,
                                           Title = x.Title
                                       }));
            }
        }

        private IQueryable<TicketsListViewModel> GetAllLaptops()
        {
            var data = this.data.Tickets.All().Select(x => new TicketsListViewModel
            {
                Author = x.Author.UserName,
                Priority = x.Priority,
                Title = x.Title,
                Id = x.Id,
                CategoryName = x.Category.Name
            }).OrderBy(x => x.Id);

            return data;
        }

        public JsonResult GetTickets([DataSourceRequest]
                                     DataSourceRequest request)
        {
            return Json(this.GetAllLaptops().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // GET: /Tickets/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", ticket.AuthorId);
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", ticket.CategoryId);
        //    return View(ticket);
        //}

        // POST: /Tickets/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(ticket).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", ticket.AuthorId);
        //    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", ticket.CategoryId);
        //    return View(ticket);
        //}

        // GET: /Tickets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticket);
        //}

        // POST: /Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Ticket ticket = db.Tickets.Find(id);
        //    db.Tickets.Remove(ticket);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }
    }
}