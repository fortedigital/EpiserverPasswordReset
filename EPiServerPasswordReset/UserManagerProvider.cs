using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ServiceLocation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace EPiServerPasswordReset
{
    public static class UserManagerProvider
    {
        public static readonly ApplicationUserManager<ApplicationUser> Manager;

        static UserManagerProvider()
        {
            var provider = new DpapiDataProtectionProvider("ResetPasswordApp");
            Manager = ServiceLocator.Current.GetInstance<ApplicationUserManager<ApplicationUser>>();
            Manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ResetPassword"));
        }
    }
}