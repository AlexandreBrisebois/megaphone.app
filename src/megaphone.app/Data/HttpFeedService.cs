using Megaphone.App.Data.Representations;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class HttpFeedService : IFeedService
    {
        private readonly HttpClient httpClient;

        public HttpFeedService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<FeedListRepresentation> GetFeeds()
        {
            var response = await httpClient.GetAsync($"http://{Environment.GetEnvironmentVariable("API-URL")}/api/feeds");

            var feedList = await response.Content.ReadFromJsonAsync<FeedListRepresentation>();

            return feedList;
        }
    }
}
