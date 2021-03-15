using Dapr.Client;
using Megaphone.App.Data.Representations;
using Megaphone.App.Data.Views;
using Megaphone.Standard.Representations.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class ResourceService
    {
        private readonly DaprClient daprClient;

        public ResourceService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public async Task<ResourceListView> GetRecent()
        {
            var date = DateTime.UtcNow;
            var path = $"api/resources/{ date.Year}/{ date.Month}/{ date.Day}";

            ResourceListRepresentation list = await GetResources(path);

            List<ResourceListRepresentation> listRepresentations = new();

            var resourceCount = 0;

            if (list.Resources.Any())
            {
                listRepresentations.Add(list);
                resourceCount += list.Resources.Count;
            }

            while (resourceCount < 40)
            {
                var previous = list.Links.FirstOrDefault(l => l.Rel == Relations.Previous);
                list = await GetResources(previous.Href);

                if (list.Resources.Any())
                {
                    listRepresentations.Add(list);
                    resourceCount += list.Resources.Count;
                }
            }

            var view = ResourceListView.Make(listRepresentations);

            return view;
        }

        private async Task<ResourceListRepresentation> GetResources(string path)
        {
            var resourceList = await daprClient.InvokeMethodAsync<ResourceListRepresentation>(HttpMethod.Get, "api", path);
           
            return resourceList;
        }
    }
}
