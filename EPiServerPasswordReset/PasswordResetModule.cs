using EPiServer.Cms.UI.AspNetIdentity;
using System.Net.Mail;

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
            var message = GetMessageBody(user, token);
            SendMail(user.Email, message);
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

        private static void SendMail(string recipientAddress, string messageBody)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(recipientAddress));
            message.Subject = "Password Reset";
            message.Body = messageBody;

            var smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        private static string GetMessageBody(ApplicationUser user, string token)
        {
            return $"Hello {user.Username}. Please follow this link to reset your password: http://localhost:54778/util/resetpassword?user={user.Id}&token={token}";
        }

    }
}