using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework.DataAnnotations;
using EpiserverAlloyPassword.Models.Pages;
using EpiserverAlloyPassword.Models.ViewModels;
using EPiServerPasswordReset;
using Microsoft.AspNet.Identity;

namespace EpiserverAlloyPassword.Controllers
{
    /// <summary>
    /// Concrete controller that handles all page types that don't have their own specific controllers.
    /// </summary>
    /// <remarks>
    /// Note that as the view file name is hard coded it won't work with DisplayModes (ie Index.mobile.cshtml).
    /// For page types requiring such views add specific controllers for them. Alterntively the Index action
    /// could be modified to set ControllerContext.RouteData.Values["controller"] to type name of the currentPage
    /// argument. That may however have side effects.
    /// </remarks>
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController : PageControllerBase<SitePageData>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ResetPasswordModule passwordResetModule;

        public DefaultPageController(UserManager<ApplicationUser> userManager, ResetPasswordModule passwordResetModule)
        {
            this.userManager = userManager;
            this.passwordResetModule = passwordResetModule;
        }

        public ViewResult Index(SitePageData currentPage)
        {
            var model = CreateModel(currentPage);
            return View(string.Format("~/Views/{0}/Index.cshtml", currentPage.GetOriginalType().Name), model);
        }

        /// <summary>
        /// Creates a PageViewModel where the type parameter is the type of the page.
        /// </summary>
        /// <remarks>
        /// Used to create models of a specific type without the calling method having to know that type.
        /// </remarks>
        private static IPageViewModel<SitePageData> CreateModel(SitePageData page)
        {
            var type = typeof(PageViewModel<>).MakeGenericType(page.GetOriginalType());
            return Activator.CreateInstance(type, page) as IPageViewModel<SitePageData>;
        }

        public async Task<ActionResult> Action()
        {
            var user =  await this.userManager.FindByNameAsync("epiadmin");
            this.passwordResetModule.SendResetPasswordMail(user);
            return Redirect("/");
        }
    }
}
