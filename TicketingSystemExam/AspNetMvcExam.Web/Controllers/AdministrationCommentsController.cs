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

namespace AspNetMvcExam.Web.Controllers
{
    public class AdministrationCommentsController : AdminController
    {
       
        // GET: /AdministrationComments/
        public ActionResult Index()
        {
            var comments = data.Comments.All().Include(c => c.Ticket).Include(c => c.User);
            return View(comments.ToList());
        }

        //// GET: /AdministrationComments/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comment comment = db.Comments.Find(id);
        //    if (comment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comment);
        //}

        // GET: /AdministrationComments/Create
        //public ActionResult Create()
        //{
        //    ViewBag.TicketId = new SelectList(db.Tickets, "Id", "AuthorId");
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
        //    return View();
        //}

        // POST: /AdministrationComments/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Comments.Add(comment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.TicketId = new SelectList(db.Tickets, "Id", "AuthorId", comment.TicketId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", comment.UserId);
        //    return View(comment);
        //}

        // GET: /AdministrationComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = data.Comments.GetById((int)id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            //ViewBag.TicketId = new SelectList(data.Tickets., "Id", "AuthorId", comment.TicketId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", comment.UserId);
            return View(comment);
        }

        // POST: /AdministrationComments/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(comment).State = EntityState.Modified;
                //var old = data.Comments.GetById(comment.Id);
                //comment.UserId = old.UserId;
                data.Comments.Update(comment);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
           
            //ViewBag.TicketId = new SelectList(db.Tickets, "Id", "AuthorId", comment.TicketId);
            ViewBag.UserId = new SelectList(data.Users.All(), "Id", "UserName");
            return View(comment);
        }

        // GET: /AdministrationComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = data.Comments.GetById((int)id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: /AdministrationComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = data.Comments.GetById(id);
            data.Comments.Delete(comment);
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }
    }
}
