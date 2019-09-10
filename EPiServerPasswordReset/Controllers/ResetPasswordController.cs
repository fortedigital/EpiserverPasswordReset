using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ServiceLocation;
using EPiServerPasswordReset.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPiServerPasswordReset.Controllers
{

    public class ResetPasswordController : Controller
    {
        public const string ResetRouteName = "resetPasswordRoute";
        public const string ErrorKey = "ResetError";

        private readonly UserManager<ApplicationUser> manager;

        public ResetPasswordController(UserManager<ApplicationUser> userManager)
        {
            this.manager = userManager;
        }

        // GET: ResetPassword
        public async Task<ActionResult> Index(string user, string token)
        {
            token = token.Replace(' ', '+');
            var isVerified = await manager.VerifyUserTokenAsync(user, "ResetPassword", token);
            if (isVerified)
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
        public async Task<ActionResult> Index(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isVerified = await manager.VerifyUserTokenAsync(model.HiddenUserId, "ResetPassword", model.HiddenToken);
                if (isVerified)
                {
                    var result = await manager.ResetPasswordAsync(model.HiddenUserId, model.HiddenToken, model.Password);
                    if(result.Succeeded)
                        return Redirect("/util/login.aspx");

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(ErrorKey, error);
                    }
                }
            }
            return View(model);
        }

        public static string GetUrlForReset(ApplicationUser user, string token)
        {
            var urlHelper = ServiceLocator.Current.GetInstance<UrlHelper>();
            var resetPasswordUrl = urlHelper.RouteUrl(ResetRouteName, new { user = user.Id, token = token }, 
                                                        System.Web.HttpContext.Current.Request.Url.Scheme);
            return resetPasswordUrl;
        }
    }
}