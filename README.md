# Reset password notification plugin for EPiServer

This plugin lets you send email notifications/reminders to users to reset their passwords.

Usage:

1. Make sure your project is set up to use ASP.NET Identity OWIN authentication:<br/>
https://world.episerver.com/documentation/developer-guides/CMS/security/episerver-aspnetidentity/ <br/>
Microsoft Docs on this topic: https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/adding-aspnet-identity-to-an-empty-or-existing-web-forms-project
2. Configure your dependency injection container:<br/>
Add implementation of ```IResetPasswordEmailTemplate``` interface to DI container.
You can use provided default template or create your own.
3. Use ```SendResetPasswordMail ``` method located in ```ResetPasswordModule``` 
on user you wish to remind of password reset. 