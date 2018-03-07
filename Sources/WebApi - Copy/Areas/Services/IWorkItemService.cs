using System.Threading.Tasks;
using Mmu.Vus.WebApi.Areas.Dtos;

namespace Mmu.Vus.WebApi.Areas.Services
{
    public interface IWorkItemService
    {
        Task UpdateWorkItemsAsync(UpdateWorkItemDto updateWorkItemDto);
    }
}