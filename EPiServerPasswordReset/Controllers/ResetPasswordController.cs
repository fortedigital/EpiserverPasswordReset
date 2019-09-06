using EPiServer.Cms.UI.AspNetIdentity;
using EPiServerPasswordReset.Models;
using System.Web.Mvc;

namespace EPiServerPasswordReset.Controllers
{

    public class ResetPasswordController : Controller
    {
        private readonly ApplicationUserManager<ApplicationUser> manager;

        public ResetPasswordController()
        {
            manager = UserManagerProvider.Manager;
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
    }
}