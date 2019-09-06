using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ServiceLocation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace EpiserverAlloyPassword
{
    public static class UserManagerProvider
    {
        public static readonly ApplicationUserManager<ApplicationUser> manager;

        static UserManagerProvider()
        {
            var provider = new DpapiDataProtectionProvider("ResetPasswordApp");
            manager = ServiceLocator.Current.GetInstance<ApplicationUserManager<ApplicationUser>>();
            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ResetPassword"));
        }
    }
}