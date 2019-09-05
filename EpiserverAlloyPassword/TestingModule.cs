using System;
using System.Linq;
using System.Web.Security;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EpiserverAlloyPassword
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [ModuleDependency(typeof(ShellInitialization))]
    public class ResetTestingModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //var manager = new ApplicationUserManager<ApplicationUser>(new UserStore<ApplicationUser>());
            var manager = ServiceLocator.Current.GetInstance<ApplicationUserManager<ApplicationUser>>();
            var user = manager.FindByNameAsync("epiadmin").Result;

        }

        public void Uninitialize(InitializationEngine context)
        {
        }

    }
}