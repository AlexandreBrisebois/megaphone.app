using Megaphone.App.Data.Representations;
using Megaphone.Standard.Time;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class HttpResourceService : ResourceService
    {
        private readonly HttpClient httpClient;

        public HttpResourceService(IClock clock, HttpClient httpClient) : base(clock)
        {
            this.httpClient = httpClient;
        }

        protected override async Task<ResourceListRepresentation> GetResources(string path)
        {
            var response = await httpClient.GetAsync($"http://{Environment.GetEnvironmentVariable("API-URL")}/{path}");

            var resourceList = await response.Content.ReadFromJsonAsync<ResourceListRepresentation>();

            return resourceList;
        }
    }
}
