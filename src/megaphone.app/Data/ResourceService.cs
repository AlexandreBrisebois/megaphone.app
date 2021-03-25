using Megaphone.App.Data.Representations;
using Megaphone.App.Data.Views;
using Megaphone.Standard.Representations.Links;
using Megaphone.Standard.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public abstract class ResourceService : IResourceService
    {
        private readonly IClock clock;

        public ResourceService(IClock clock)
        {
            this.clock = clock;
        }
        public async Task<ResourceListView> GetRecent()
        {
            var date = clock.Now.UtcDateTime;
            var path = $"api/resources/{ date.Year}/{ date.Month}/{ date.Day}";

            ResourceListRepresentation list = await GetResources(path);

            List<ResourceListRepresentation> listRepresentations = new();

            var resourceCount = 0;

            if (list.Resources.Any())
            {
                listRepresentations.Add(list);
                resourceCount += list.Resources.Count;
            }

            var days = 0;

            while (resourceCount < 40 && days < 15)
            {
                var previous = list.Links.FirstOrDefault(l => l.Rel == Relations.Previous);
                list = await GetResources(previous.Href);

                if (list.Resources.Any())
                {
                    listRepresentations.Add(list);
                    resourceCount += list.Resources.Count;
                }

                days++;
            }

            var view = ResourceListView.Make(listRepresentations);

            return view;
        }
        protected abstract Task<ResourceListRepresentation> GetResources(string path);
    }
}
