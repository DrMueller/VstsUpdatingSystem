using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Mmu.Vus.WebApi.Areas.Dtos;

namespace Mmu.Vus.WebApi.Areas.Services.Implementation
{
    public class WorkItemService : IWorkItemService
    {
        private const string TfsStateMergedToMaster = "Merged to Master";
        private const string BaseUrl = "https://drmueller.visualstudio.com/api/";

        public async Task UpdateWorkItemsAsync(UpdateWorkItemDto updateWorkItemDto)
        {
            //var connection = new VssConnection(new Uri(BaseUrl), new Microsoft.TeamFoundation.s()

            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            var wiqlQuery = $"SELECT [System.Id],[System.WorkItemType],[System.Title],[System.AssignedTo],[System.State],[System.Tags] FROM WorkItems WHERE  [System.State] = '{TfsStateMergedToMaster}'";

            var wiql = new QueryHierarchyItem
            {
                Wiql = wiqlQuery,
                IsFolder = false
            };

            var tra = new Wiql();
            tra.Query = wiqlQuery;

            var workitems = await witClient.QueryByWiqlAsync(tra);
        }
    }
}