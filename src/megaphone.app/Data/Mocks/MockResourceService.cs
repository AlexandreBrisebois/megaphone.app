using Megaphone.App.Data.Representations;
using Megaphone.Standard.Time;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public class MockResourceService : ResourceService
    {
        public MockResourceService(IClock clock) : base(clock)
        {

        }

        protected override async Task<ResourceListRepresentation> GetResources(string path)
        {
            var resourceList = await JsonSerializer.DeserializeAsync<ResourceListRepresentation>(utf8Json: File.OpenRead("./mock-data/" + path + "/resources.json"));

            return resourceList;
        }
    }
}