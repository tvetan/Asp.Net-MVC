using System.Collections.Generic;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using AspNetMvcExam.Models;
using AspNetMvcExam.Web.Models;

namespace AspNetMvcExam.Web.Controllers
{
    public class AdministrationCategoriesController : AdminController
    {
        // GET: /AdministrationCategories/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadCategory([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.data.Categories.All()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel comment)
        {
            var commentDb = this.data.Categories.GetById(comment.Id);

            commentDb.Name = comment.Name;
            this.data.SaveChanges();

            return Json(new[] { comment }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyCategory([DataSourceRequest] DataSourceRequest request, Category category)
        {
            
            //foreach (var ticket in category.Tickets)
            //{
            //    foreach (var comment in ticket.Comments)
            //    {
            //        this.data.Comments.Delete(comment);
            //    }

            //    this.data.Tickets.Delete(ticket);
            //  }

            var tickets = category.Tickets;
            for (int i = 0; i < tickets.Count; i++)
            {
                Ticket currentTicket = tickets.ElementAt(i);
                var comments = currentTicket.Comments;
                for (int j = 0; j < comments.Count; j++)
                {
                    this.data.Comments.Delete(comments.ElementAt(j));
                }

                this.data.Tickets.Delete(currentTicket);
            }

            this.data.Categories.Delete(category);

            this.data.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCategory([DataSourceRequest]DataSourceRequest request, CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {

                // Create a new Product entity and set its properties from the posted ProductViewModel
                var entity = new Category
                {
                    Name = category.Name,
                };
                data.Categories.Add(entity);
                data.SaveChanges();


            }
            // Return the inserted product. The grid needs the generated ProductID. Also return any validation errors.
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }
    }
}