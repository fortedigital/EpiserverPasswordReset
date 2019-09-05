using EPiServer.Core;

namespace EpiserverAlloyPassword.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
