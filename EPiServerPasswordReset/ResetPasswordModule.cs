using EPiServer.Cms.UI.AspNetIdentity;
using EPiServerPasswordReset.Controllers;
using EPiServerPasswordReset.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Net.Mail;
using System.Net.Mime;

namespace EPiServerPasswordReset
{
    public class ResetPasswordModule
    {
        private readonly UserManager<ApplicationUser> manager;
        private readonly IResetPasswordEmailTemplate emailTemplate;

        public ResetPasswordModule(UserManager<ApplicationUser> userManager, IResetPasswordEmailTemplate template)
        {
            this.emailTemplate = template;
            this.manager = userManager;
        }

        public async void SendResetPasswordMail(ApplicationUser user)
        {
            if (user == null) throw new ArgumentNullException();

            var token = await manager.GeneratePasswordResetTokenAsync(user.Id);
            SendMail(user, token);
        }

        private void SendMail(ApplicationUser user, string token)
        {
            var content = this.emailTemplate.GetEmailContent(user.Username, ResetPasswordController.GetUrlForReset(user, token));

            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));
            message.Subject = content.Subject;
            CreateMessageViews(message, content);

            var smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        private void CreateMessageViews(MailMessage message, ResetPasswordEmailContent content)
        {
            if (content.HtmlBody == null && content.TextBody == null)
                throw new NullReferenceException("Both body properties in content object were null");

            if(content.TextBody != null)
            {
                message.Body = content.TextBody;
                if(content.HtmlBody != null){
                    var mimeType = new ContentType("text/html");
                    var alternateView = AlternateView.CreateAlternateViewFromString(content.HtmlBody, mimeType);
                    message.AlternateViews.Add(alternateView);
                }
            }
            else
            {
                message.Body = content.HtmlBody;
                message.IsBodyHtml = true;
            }
        }
    }
}