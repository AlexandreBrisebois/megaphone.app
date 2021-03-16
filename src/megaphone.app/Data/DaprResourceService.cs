using Dapr.Client;
using Megaphone.App.Data.Representations;
using Megaphone.Standard.Time;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class DaprResourceService : ResourceService
    {
        private readonly DaprClient daprClient;

        public DaprResourceService(IClock clock, DaprClient daprClient) : base(clock)
        {
            this.daprClient = daprClient;
        }

        protected override async Task<ResourceListRepresentation> GetResources(string path)
        {
            var resourceList = await daprClient.InvokeMethodAsync<ResourceListRepresentation>(HttpMethod.Get, "api", path);

            return resourceList;
        }
    }
}
