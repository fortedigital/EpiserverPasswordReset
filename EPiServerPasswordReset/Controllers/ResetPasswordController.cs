using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ServiceLocation;
using EPiServerPasswordReset.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace EPiServerPasswordReset.Controllers
{

    public class ResetPasswordController : Controller
    {
        public const string ResetRouteName = "resetPasswordRoute";

        private readonly UserManager<ApplicationUser> manager;

        public ResetPasswordController(UserManager<ApplicationUser> userManager)
        {
            this.manager = userManager;
        }

        // GET: ResetPassword
        public ActionResult Index(string user, string token)
        {
            token = token.Replace(' ', '+');
            if(manager.VerifyUserTokenAsync(user, "ResetPassword", token).Result)
            {
                var model = new ResetPasswordViewModel()
                {
                    HiddenUserId = user,
                    HiddenToken = token
                };
                return View(model);
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(manager.VerifyUserTokenAsync(model.HiddenUserId, "ResetPassword", model.HiddenToken).Result)
                {
                    manager.ResetPasswordAsync(model.HiddenUserId, model.HiddenToken, model.Password);
                    return Redirect("/util/login.aspx");
                }
            }
            return View(model);
        }

        public static string GetUrlForReset(ApplicationUser user, string token)
        {
            var urlHelper = ServiceLocator.Current.GetInstance<UrlHelper>();
            var resetPasswordUrl = urlHelper.RouteUrl(ResetRouteName, new { user = user.Id, token = token }, System.Web.HttpContext.Current.Request.Url.Scheme);
            return resetPasswordUrl;
        }
    }
}