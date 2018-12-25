using System.Collections.Generic;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow.Interfaces;

namespace SpawnTraffic.Workflow
{
    public class MenuWorkflow : IMenuWorkflow
    {
        public Menu GetMenu()
        {
            return new Menu
            {
                Options = new List<string>
                {
                    "Add Skater",
                    "List Skaters"
                }
            };
        }
    }
}
