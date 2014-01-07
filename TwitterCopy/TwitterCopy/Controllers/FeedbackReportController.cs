namespace TwitterCopy.Controllers
{
    using System.Web.Mvc;

    using TwitterCopy.Controllers.Base;
    using TwitterCopy.Data;
    using TwitterCopy.Models;
    using TwitterCopy.Models.FeedbackReportModels;

    [Authorize]
    public class FeedbackReportController : BaseController
    {
        public FeedbackReportController(ITwitterCopyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var logInUser = this.GetLogInUser();

            var feedbackReportViewModel = new FeedbackReportViewModel()
            {
                Username = logInUser.UserName,
                Email = logInUser.Email,
                Type = Models.FeedbackReportType.InactiveUsernames
            };

            return this.View(feedbackReportViewModel);
        }

        [HttpPost]
        public ActionResult Index(FeedbackReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedbackReport = new FeedbackReport()
                {
                    IsFixed = false,
                    IsDeleted = false,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    User = this.GetLogInUser(),
                    Subject = model.Subject,
                    FullName = model.FullName,
                    Type = model.Type                    
                };

                this.Data.FeedbackReports.Add(feedbackReport);
                this.Data.SaveChanges();

                this.TempData["InfoMessage"] = "The Report was succesfully uploaded";

                return this.RedirectToAction("index", "home");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Submitted()
        {
            return this.View();
        }
    }
}