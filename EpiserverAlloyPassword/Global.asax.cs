using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EpiserverAlloyPassword
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        
            RouteTable.Routes.MapRoute(
                    "resetPasswordRoute",
                    "util/resetpassword",
                    new { controller = "ResetPassword", action = "Index", id = UrlParameter.Optional }
                );

            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }
    }
}