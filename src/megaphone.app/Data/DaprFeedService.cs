using Dapr.Client;
using Megaphone.App.Data.Representations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class DaprFeedService : IFeedService
    {
        private readonly DaprClient daprClient;

        public DaprFeedService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public async Task<FeedListRepresentation> GetFeeds()
        {
            var feedList = await daprClient.InvokeMethodAsync<FeedListRepresentation>(HttpMethod.Get, "api", "api/feeds");

            return feedList;
        }
    }
}
