using Megaphone.App.Data.Representations;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class FeedService
    {
        private readonly HttpClient httpClient;

        public FeedService() => this.httpClient = new HttpClient();

        public async Task<FeedListRepresentation> GetFeeds()
        {
            var httpResponse = await httpClient.GetAsync("http://HD:40002/v1.0/invoke/api/method/api/feeds");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var feedList = JsonSerializer.Deserialize<FeedListRepresentation>(content);

            return feedList;
        }
    }
}
