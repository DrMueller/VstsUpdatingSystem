using System.Threading.Tasks;

namespace Mmu.Vus.WebApi.Areas.Services
{
    public interface IWorkItemUpdateService
    {
        Task UpdateWorkItemsAsync(float version);
    }
}