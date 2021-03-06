﻿namespace TwitterCopy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;

    using TwitterCopy.Common;
    using TwitterCopy.Controllers.Base;
    using TwitterCopy.Data;
    using TwitterCopy.Models;
    using TwitterCopy.Models.Account;
    using TwitterCopy.Web.Common;

    public class AccountController : BaseController
    {
        public AccountController(ITwitterCopyData data)
            : this(data, new TwitterCopyUserManager<ApplicationUser>(new UserStore<ApplicationUser>(data.Context.DbContext)))
        {
        }

        public AccountController(ITwitterCopyData data, UserManager<ApplicationUser> userManager)
            : base(data)
        {
            this.UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [Authorize]
        public ActionResult Settings()
        {
            var user = this.GetLogInUser();

            SettingsViewModel settingsViewModel = new SettingsViewModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                LanguageId = user.LanguageId,
                Language = user.Language,
                CountryId = user.CountryId,
                Country = user.Country,
                TimeZoneId = user.TimeZoneId,
                TimeZone = user.TimeZone
            };

            var countries = this.Data.Countries.All();
            settingsViewModel.Countries = countries;

            var languages = this.Data.Languages.All();
            settingsViewModel.Languages = languages;

            var timeZones = this.Data.TimeZones.All();
            settingsViewModel.TimeZones = timeZones;

            return this.View(settingsViewModel);
        }

        private IList<SelectListItem> CreateSelectList(IEnumerable<Selectable> selectableList)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var selectable in selectableList)
            {
                items.Add(new SelectListItem { Text = selectable.Name, Value = selectable.Code });
            }

            return items;
        }


        public ActionResult LoginRegister()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(SettingsViewModel user)
        {
            if (ModelState.IsValid)
            {
                var logInUser = this.GetLogInUser();

                logInUser.Email = user.Email;
                logInUser.UserName = user.UserName;
                logInUser.LanguageId = user.LanguageId;
                logInUser.Language = user.Language;
                logInUser.CountryId = user.CountryId;
                logInUser.Country = user.Country;
                logInUser.TimeZoneId = user.TimeZoneId;
                logInUser.TimeZone = user.TimeZone;

                this.Data.Users.Update(logInUser);
                this.Data.SaveChanges();

                return RedirectToAction("Index", "home");
            }

            return this.View(user);
        }

        [Authorize]
        public ActionResult Profile()
        {
            var user = this.GetLogInUser();
            var userProfile = new UserProfileViewModel()
            {
                Id = user.Id,
                ProfilePictureId = user.ProfilePictureId,
                Bio = user.Bio,
                Username = user.UserName,
                Name = user.FullName,
                Website = user.WebSite ?? "http://",
                Location = user.City
            };

            return this.View(userProfile);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(UserProfileViewModel model)
        {
            if (model.Id != User.Identity.GetUserId())
            {
                ModelState.AddModelError(string.Empty, "Not a valid user");
            }

            if (ModelState.IsValid)
            {
                var user = this.GetLogInUser();

                if (model.ProfilePicture != null)
                {
                    var userProfilePicture = this.ExtractPicture(model.ProfilePicture.InputStream,
                    model.ProfilePicture.FileName,
                    model.ProfilePicture.ContentLength,
                    model.ProfilePicture.ContentType);
                    if (user.ProfilePicture != null)
                    {
                        var oldUserProfilePicture = user.ProfilePicture;
                        this.Data.Documents.Delete(oldUserProfilePicture);
                    }

                    user.ProfilePicture = userProfilePicture;
                }

                user.Bio = model.Bio;
                user.City = model.Location;
                user.WebSite = model.Website;
                user.FullName = model.Name;

                this.Data.Users.Update(user);
                this.Data.SaveChanges();

                this.TempData["InfoMessage"] = "Profile was edited succesfully";

                return this.RedirectToAction("index", "home");
            }

            return this.View(model);
        }

        private Document ExtractPicture(Stream stream, string name, int size, string type)
        {
            byte[] documentBytes = new byte[stream.Length];
            stream.Read(documentBytes, 0, documentBytes.Length);

            Document fileModel = new Document()
            {
                Content = documentBytes,
                Size = size,
                Type = type,
            };

            return fileModel;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);

                //user.CreatedOn = DateTime.Today;
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.Data.Users.All().Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already registered");
            }

            if (this.Data.Users.All().Any(x => x.UserName == model.UserName))
            {
                ModelState.AddModelError("Username", "Username already registered");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() {
                    UserName = model.UserName, 
                    Email = model.Email, 
                    FullName = model.FullName 
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                                   message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                                   : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                                     : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                                       : message == ManageMessageId.Error ? "An error has occurred."
                                         : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");

            return this.View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                return this.View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }

            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;

            return this.View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                this.UserManager.Dispose();
                this.UserManager = null;
            }

            base.Dispose(disposing);
        }

        public ActionResult ForgottenPassword()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ForgottenPassword(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                this.ModelState.AddModelError("userEmail", "User email is required");
            }

            var user = this.Data.Users.GetByEmail(userEmail);

            if (user == null)
            {
                this.ModelState.AddModelError("userEmail", "The is no user with this email");
            }
            else
            {
                user.ForgottenPasswordToken = Guid.NewGuid();
                this.Data.SaveChanges();
                this.SendForgottenPasswordToUser(user);
                this.TempData["InfoMessage"] = "Email was sent";

                return this.RedirectToAction("ForgottenPassword");
            }

            return this.View();
        }

        private void SendForgottenPasswordToUser(ApplicationUser user)
        {
            var mailSender = MailSender.Instance;

            var forgottenPasswordEmailTitle = string.Format("Forgotten Password for user {0}", user.UserName);

            var forgottenPasswordEmailBody = string.Format("Click the link to restore password <a href='{0}'>{0}</a>",
                Url.Action("ChangePassword", "Account", new { token = user.ForgottenPasswordToken }, Request.Url.Scheme));

            mailSender.SendMail(user.Email, forgottenPasswordEmailTitle, forgottenPasswordEmailBody);
        }

        public ActionResult ChangePassword(string token)
        {
            Guid guid;
            if (!Guid.TryParse(token, out guid))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid token!");
            }

            var user = this.Data.Users.All().FirstOrDefault(x => x.ForgottenPasswordToken == guid);

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid token!");
            }

            var forgottenPasswordModel = new ForgottenPasswordViewModel
            {
                Token = guid
            };

            return this.View(forgottenPasswordModel);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ForgottenPasswordViewModel model)
        {
            var user = this.Data.Users.All().FirstOrDefault(x => x.ForgottenPasswordToken == model.Token);

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid token!");
            }

            if (ModelState.IsValid)
            {
                IdentityResult removePassword = await this.UserManager.RemovePasswordAsync(user.Id);

                if (removePassword.Succeeded)
                {
                    IdentityResult changePassword = await this.UserManager.AddPasswordAsync(user.Id, model.Password);

                    if (changePassword.Succeeded)
                    {
                        user.ForgottenPasswordToken = null;
                        this.Data.SaveChanges();

                        this.TempData["InfoMessage"] = "Password succefuly changed";
                        return this.RedirectToAction("Login");
                    }

                    this.AddErrors(changePassword);
                }

                this.AddErrors(removePassword);
            }

            return this.View(model);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}