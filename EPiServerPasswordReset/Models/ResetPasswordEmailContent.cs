namespace EPiServerPasswordReset.Models
{
    public class ResetPasswordEmailContent
    {
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string TextBody { get; set; }
        
    }
}
