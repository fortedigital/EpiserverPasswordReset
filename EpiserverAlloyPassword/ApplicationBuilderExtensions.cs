using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Shell.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverAlloyPassword
{
    public static class ApplicationBuilderExtensions
    {
        public static IAppBuilder SetupCustomAspNetIdentity<TUser>(this IAppBuilder app) where TUser : IdentityUser, IUIUser, new()
        {
            var applicationOptions = new ApplicationOptions
            {
                DataProtectionProvider = app.GetDataProtectionProvider()
            };

            // Configure the db context, user manager and signin manager to use a single instance per request by using
            // the default create delegates
            app.CreatePerOwinContext<ApplicationOptions>(() => applicationOptions);
            app.CreatePerOwinContext<ApplicationDbContext<TUser>>(ApplicationDbContext<TUser>.Create);
            app.CreatePerOwinContext<ApplicationRoleManager<TUser>>(ApplicationRoleManager<TUser>.Create);
            app.CreatePerOwinContext<ApplicationUserManager<TUser>>(ApplicationUserManager<TUser>.Create);
            app.CreatePerOwinContext<ApplicationSignInManager<TUser>>(ApplicationSignInManager<TUser>.Create);

            // Configure the application 
            app.CreatePerOwinContext<UIUserProvider>(ApplicationUserProvider<TUser>.Create);
            app.CreatePerOwinContext<UIRoleProvider>(ApplicationRoleProvider<TUser>.Create);
            app.CreatePerOwinContext<UIUserManager>(ApplicationUIUserManager<TUser>.Create);
            app.CreatePerOwinContext<UISignInManager>(ApplicationUISignInManager<TUser>.Create);

            // Saving the connection string in the case dbcontext be requested from none web context
            ConnectionStringNameResolver.ConnectionStringNameFromOptions = applicationOptions.ConnectionStringName;

            return app;
        }
    }
}