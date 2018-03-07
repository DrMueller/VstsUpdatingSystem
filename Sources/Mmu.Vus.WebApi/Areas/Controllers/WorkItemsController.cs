using System.Threading.Tasks;
using System.Web.Http;
using Mmu.Vus.WebApi.Areas.Dtos;
using Mmu.Vus.WebApi.Areas.Services;
using Mmu.Vus.WebApi.Areas.Services.Implementation;

namespace Mmu.Vus.WebApi.Areas.Controllers
{
    [RoutePrefix("api/WorkItems")]
    public class WorkItemsController : ApiController
    {
        private readonly IWorkItemUpdateService _workItemUpdateService;

        public WorkItemsController(IWorkItemUpdateService workItemUpdateService)
        {
            _workItemUpdateService = workItemUpdateService;
        }

        [HttpPost]
        [Route("UpdateVersions")]
        public async Task<IHttpActionResult> UpdateVersionsAsync([FromBody] UpdateWorkItemsDto dto)
        {
            await _workItemUpdateService.UpdateWorkItemsAsync(dto.VersionNumber);
            return Ok();
        }
    }
}