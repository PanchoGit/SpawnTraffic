using System;
using SpawnTraffic.AppCmd.Resources;
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

            var menu = menuWorkflow.GetMenu().Data;

            Console.WriteLine();

            foreach (var item in menu.Options)
            {
                Console.WriteLine(ViewResource.MenuOptionItem, keyIndex++, item);
            }

            Console.WriteLine();
        }
    }
}
