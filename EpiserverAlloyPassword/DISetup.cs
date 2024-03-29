﻿using System.Data.Entity;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServerPasswordReset;
using EPiServerPasswordReset.Templates;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}