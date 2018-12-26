using System.Collections.Generic;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow.Interfaces;
using SpawnTraffic.Workflow.Resources;

namespace SpawnTraffic.Workflow
{
    public class MenuWorkflow : IMenuWorkflow
    {
        public Result<Menu> GetMenu()
        {
            var menu = new Menu
            {
                Options = new List<string>
                {
                    WorkflowResource.MenuAddSkater,

                    WorkflowResource.MenuListSkaters
                }
            };

            return new Result<Menu>(menu);
        }
    }
}
