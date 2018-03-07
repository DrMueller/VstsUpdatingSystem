using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace Mmu.Vus.WebApi.Areas.Services.Implementation
{
    public class WorkItemUpdateService : IWorkItemUpdateService
    {
        private const string BaseUrl = "https://drmueller.visualstudio.com/";
        private const string PersonalAccessToken = "6oeknwgibfgwdsm563ft6eajjjqjblxpdzex3rrsvk7kz7wongkq";
        private const string TfsStateMergedToMaster = "Merged to Master";

        public async Task UpdateWorkItemsAsync(float version)
        {
            var connection = new VssConnection(new Uri(BaseUrl), new VssBasicCredential(string.Empty, PersonalAccessToken));
            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            var workItems = await GetWorkItemsReadyToMergeAsync(witClient);

            var updateTasks = workItems.Select(f => UpdateWorkItemAsync(f, version, witClient));
            await Task.WhenAll(updateTasks);
        }

        private static async Task<IReadOnlyCollection<WorkItem>> GetWorkItemsReadyToMergeAsync(WorkItemTrackingHttpClient witClient)
        {
            var wiqlQuery = $"SELECT [System.Id],[System.WorkItemType],[System.Title],[System.AssignedTo],[System.State],[System.Tags] FROM WorkItems WHERE  [System.State] = '{TfsStateMergedToMaster}'";
            var wiql = new Wiql { Query = wiqlQuery };
            var queryResult = await witClient.QueryByWiqlAsync(wiql);
            var workItemIds = queryResult.WorkItems.Select(f => f.Id).ToList();
            
            if (!workItemIds.Any())
            {
                return new List<WorkItem>();    
            }

            var result = await witClient.GetWorkItemsAsync(workItemIds);

            return result;
        }

        private static async Task UpdateWorkItemAsync(WorkItem workItem, float version, WorkItemTrackingHttpClientBase witClient)
        {
            var jsonPatchDocument = new JsonPatchDocument
            {
                new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.State",
                    Value = "Done"
                },
                new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = "/fields/ScrumforHeroAG.DeployedVersion",
                    Value = version
                }
            };

            await witClient.UpdateWorkItemAsync(jsonPatchDocument, workItem.Id.Value);
        }
    }
}