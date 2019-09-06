using EPiServer.Cms.UI.AspNetIdentity;
using MailKit.Net.Smtp;
using MimeKit;

namespace EPiServerPasswordReset
{
    public class PasswordResetModule
    {

        private readonly ApplicationUserManager<ApplicationUser> manager;

        public PasswordResetModule()
        {
            manager = UserManagerProvider.Manager;
        }

        public void SendResetPasswordMail(ApplicationUser user)
        {
            if (user == null) return;

            var token = manager.GeneratePasswordResetTokenAsync(user.Id).Result;
            SendMail(user, token);
        }

        //TODO: to be removed
        public void Run()
        {
            var user = GetApplicationUser("epiadmin");
            SendResetPasswordMail(user);
        }

        //TODO: to be removed
        private ApplicationUser GetApplicationUser(string name)
        {
            return manager.FindByNameAsync(name).Result;
        }

        private static void SendMail(ApplicationUser user, string token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("mikolaj.dlus@fortedigital.no"));
            //message.To.Add(new MailboxAddress(user.Email));
            message.To.Add(new MailboxAddress("mikolaj.dlus@fortedigital.no"));
            message.Subject = "Password Reset";
            message.Body = new TextPart("plain")
            {
                Text = GetMessageBody(user, token)
            };
            //simulate sending email
            //using (var stream = System.IO.File.OpenWrite("E:/Dev/Repos/mail_log.txt"))
            //{
            //    message.WriteTo(stream);
            //}
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);
                client.Authenticate("mikolaj.dlus@fortedigital.no", "M$970920");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private static string GetMessageBody(ApplicationUser user, string token)
        {
            return $"Hello {user.Username}. Please follow this link to reset your password: http://localhost:54778/util/resetpassword?user={user.Id}&token={token}";
        }

    }
}