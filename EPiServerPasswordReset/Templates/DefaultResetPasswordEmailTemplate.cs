using EPiServerPasswordReset.Models;

namespace EPiServerPasswordReset.Templates
{
    public class DefaultResetPasswordEmailTemplate : IResetPasswordEmailTemplate
    {
        public ResetPasswordEmailContent GetEmailContent(string userName, string resetPasswordUrl)
        {
            var content = new ResetPasswordEmailContent()
            {
                Subject = "Reset Password",
                HtmlBody = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" +
                    $"<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\"></HEAD>" +
                    $"<BODY><h2>Hello {userName}</h2><br/><div>Please follow this link to reset your password:<a href=\"{resetPasswordUrl}\">Link</a></div></BODY></HTML>",
                TextBody = $"Hello {userName}. Please follow this link to reset your password: {resetPasswordUrl}"
            };
            return content;
        }
    }
}
