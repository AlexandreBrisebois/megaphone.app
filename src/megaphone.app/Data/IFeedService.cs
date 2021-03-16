using Megaphone.App.Data.Representations;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public interface IFeedService
    {
        Task<FeedListRepresentation> GetFeeds();
    }
}