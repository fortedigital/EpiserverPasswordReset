using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EPiServerPasswordReset
{
    [InitializableModule]
    public class ResetPasswordRouteInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {

            RouteTable.Routes.MapRoute(
                    "resetPasswordRoute",
                    "util/resetpassword",
                    new { controller = "ResetPassword", action = "Index", id = UrlParameter.Optional }
                );
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}