using EPiServerPasswordReset.Models;

namespace EPiServerPasswordReset.Templates
{
    public interface IResetPasswordEmailTemplate
    {
        ResetPasswordEmailContent GetEmailContent(string userName, string resetPasswordUrl);
    }
}
