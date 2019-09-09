using System.Data.Entity;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServerPasswordReset;
using EPiServerPasswordReset.Templates;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace EpiserverAlloyPassword
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyInjectionSetup : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            context.Services.AddTransient<DbContext, ApplicationDbContext<ApplicationUser>>();
            context.StructureMap().Configure(ctx => { ctx.For<IResetPasswordEmailTemplate>().Use<DefaultResetPasswordEmailTemplate>(); });
            context.StructureMap().Configure(ctx => { ctx.ForConcreteType<PasswordResetModule>(); });
            context.StructureMap().Configure(ctx => { ctx.ForConcreteType<DpapiDataProtectionProvider>().Configure.Ctor<string>("appName").Is("ResetPasswordApp"); });
            context.StructureMap().Configure(ctx => { ctx.For<IDataProtector>().Use(ct => ct.GetInstance<DpapiDataProtectionProvider>().Create("ResetPassword")); });
            context.StructureMap().Configure(ctx => { ctx.For<IUserTokenProvider<ApplicationUser, string>>().Use<DataProtectorTokenProvider<ApplicationUser>>(); });
            context.StructureMap().Configure(ctx =>
            {
                ctx.ForConcreteType<UserManager<ApplicationUser>>().Configure.
                OnCreation("Adding token provider", (ct, m) => m.UserTokenProvider = ct.GetInstance<DataProtectorTokenProvider<ApplicationUser>>());
            });
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}