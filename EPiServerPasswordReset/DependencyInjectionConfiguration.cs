using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace EPiServerPasswordReset
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DependencyInjectionConfiguration : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(ctx => { ctx.ForConcreteType<ResetPasswordModule>(); });
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
