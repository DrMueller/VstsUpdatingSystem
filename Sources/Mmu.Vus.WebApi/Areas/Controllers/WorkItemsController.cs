using System;
using System.Threading.Tasks;
using System.Web.Http;
using Mmu.Vus.WebApi.Areas.Dtos;
using Mmu.Vus.WebApi.Areas.Services;

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

        [HttpGet]
        [Route("HelloWorld")]
        public IHttpActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        [Route("UpdateVersions")]
        public async Task<IHttpActionResult> UpdateVersionsAsync([FromBody] UpdateWorkItemsDto dto)
        {
            try
            {
                await _workItemUpdateService.UpdateWorkItemsAsync(dto.VersionNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                {
                    innerEx = innerEx.InnerException;
                }

                var str = innerEx.Message + ": " + innerEx.StackTrace;
                return Ok(str);
            }
        }
    }
}