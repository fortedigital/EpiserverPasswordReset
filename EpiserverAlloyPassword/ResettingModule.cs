using EPiServer.Cms.UI.AspNetIdentity;
using MimeKit;

namespace EpiserverAlloyPassword
{
    public class ResettingModule
    {

        private readonly ApplicationUserManager<ApplicationUser> manager;

        public ResettingModule()
        {
            manager = UserManagerProvider.manager;
        }

        public void Run()
        {
            var user = GetApplicationUser("epiadmin");
            var id = GetUserId(user);
            var token = manager.GeneratePasswordResetTokenAsync(id).Result;
            SendResetPassword(user, token);
        }

        public ApplicationUser GetApplicationUser(string name)
        {
            return manager.FindByNameAsync(name).Result;
        }

        public string GetUserId(ApplicationUser user)
        {
            return user.Id;
        }

        public void SendResetPassword(ApplicationUser user, string token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("admin@episerver.com"));
            message.To.Add(new MailboxAddress(user.Email));
            message.Subject = "Password Reset";
            message.Body = new TextPart(GetMessageBody(user, token));
            //simulate sending email
            using (var stream = System.IO.File.OpenWrite("E:/Dev/Repos/mail_log.txt"))
            {
                message.WriteTo(stream);
            }
        }

        private string GetMessageBody(ApplicationUser user, string token)
        {
            return $"Hello {user.Username}. Please follow this link to reset your password: http://localhost:54778/util/resetpassword?user={user.Id}&token={token}";
        }

    }
}