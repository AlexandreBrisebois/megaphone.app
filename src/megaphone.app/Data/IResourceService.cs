using Megaphone.App.Data.Views;
using System.Threading.Tasks;

namespace Megaphone.App.Data
{
    public interface IResourceService
    {
        Task<ResourceListView> GetRecent();
    }
}