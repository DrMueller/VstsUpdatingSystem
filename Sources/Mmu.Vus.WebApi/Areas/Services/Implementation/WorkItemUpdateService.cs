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
        private const string PersonalAccessToken = "dqje4rk6kmvi7qfmyq2fipll6cdk5uqlolzp2pxguhpynxd6r35a";
        private const string TfsStateMergedToMaster = "Merged to Master";

        public async Task UpdateWorkItemsAsync(string version)
        {
            var connection = new VssConnection(new Uri(BaseUrl), new VssBasicCredential(string.Empty, PersonalAccessToken));
            var witClient = connection.GetClient<WorkItemTrackingHttpClient>();

            var workItems = await GetWorkItemsReadyToMergeAsync(witClient);

            var updateTasks = workItems.Select(f => UpdateWorkItemAsync(f, version, witClient));
            await Task.WhenAll(updateTasks);
        }

        private static async Task<IReadOnlyCollection<WorkItem>> GetWorkItemsReadyToMergeAsync(WorkItemTrackingHttpClient witClient)
        {
            var wiqlQuery = $"SELECT [System.Id],[System.WorkItemType],[System.Title],[System.AssignedTo],[System.State],[System.Tags] FROM WorkItems WHERE [System.State] = '{TfsStateMergedToMaster}' AND ScrumforHeroAG.DeployedVersion = ''";
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

        private static async Task UpdateWorkItemAsync(WorkItem workItem, string version, WorkItemTrackingHttpClientBase witClient)
        {
            var jsonPatchDocument = new JsonPatchDocument
            {
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