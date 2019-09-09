namespace EPiServerPasswordReset
{
    public interface IResetPasswordEmailTemplate
    {
        ResetPasswordEmailContent GetEmailContent(string userName, string resetPasswordUrl);
    }
}
