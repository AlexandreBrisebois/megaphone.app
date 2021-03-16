using Megaphone.App.Data.Representations;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class MockFeedService : IFeedService
    {
        public MockFeedService()
        {
        }

        public async Task<FeedListRepresentation> GetFeeds()
        {
            var feedList = await JsonSerializer.DeserializeAsync<FeedListRepresentation>(utf8Json: File.OpenRead("./mock-data/api/feeds/list.json"));

            return feedList;
        }
    }
}
