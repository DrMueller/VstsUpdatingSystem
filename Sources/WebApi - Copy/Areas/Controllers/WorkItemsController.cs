using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Vus.WebApi.Areas.Dtos;

namespace Mmu.Vus.WebApi.Areas.Controllers
{
    [Route("api/[controller]")]
    public class WorkItemsController : Controller
    {
        public WorkItemsController()
        {
            
        }

        [HttpPut("UpdateMerged")]
        public void Put(UpdateWorkItemDto updateWorkItemDto)
        {
        }
    }
}
