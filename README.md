# Reset password notification plugin for EPiServer

This plugin lets you send email notifications/reminders to users to reset their passwords.

Usage:

1. Make sure your project is set up to use ASP.NET Identity
2. Configure your dependency injection container:<br/>
Add implementation of ```IResetPasswordEmailTemplate``` interface to DI container.
You can use provided default template or create your own.
3. Use ```SendResetPasswordMail ``` method located in ```ResetPasswordModule``` 
on user you wish to remind of password reset. 