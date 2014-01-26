using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TwitterCopy.Models;
using TwitterCopy.Data;

namespace TwitterCopy.Controllers
{
    public class testController : Controller
    {
        private TwitterCopyDbContext db = new TwitterCopyDbContext();

        // GET: /test/
        public ActionResult Index()
        {
            var tweets = db.Tweets.Include(t => t.Author);
            return View(tweets.ToList());
        }

        // GET: /test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // GET: /test/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: /test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,AuthorId,Status,CreatedOn,ModifiedOn")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Tweets.Add(tweet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", tweet.AuthorId);
            return View(tweet);
        }

        // GET: /test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", tweet.AuthorId);
            return View(tweet);
        }

        // POST: /test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,AuthorId,Status,CreatedOn,ModifiedOn")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", tweet.AuthorId);
            return View(tweet);
        }

        // GET: /test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        // POST: /test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tweet tweet = db.Tweets.Find(id);
            db.Tweets.Remove(tweet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
