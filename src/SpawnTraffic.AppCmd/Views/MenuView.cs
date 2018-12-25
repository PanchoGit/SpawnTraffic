using System;
using SpawnTraffic.AppCmd.Views.Interfaces;
using SpawnTraffic.Workflow.Interfaces;

namespace SpawnTraffic.AppCmd.Views
{
    public class MenuView : IMenuView
    {
        private readonly IMenuWorkflow menuWorkflow;

        public MenuView(IMenuWorkflow menuWorkflow)
        {
            this.menuWorkflow = menuWorkflow;
        }

        public void DisplayMenu()
        {
            var keyIndex = 1;
            Console.WriteLine();
            foreach (var item in menuWorkflow.GetMenu().Options)
            {
                Console.WriteLine($"{keyIndex++} > {item}");
            }
            Console.WriteLine();
        }
    }
}
