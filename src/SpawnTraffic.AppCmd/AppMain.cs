using System;
using System.Collections.Generic;
using SpawnTraffic.AppCmd.Resources;
using SpawnTraffic.AppCmd.Views.Interfaces;

namespace SpawnTraffic.AppCmd
{
    public class AppMain
    {
        private readonly IMenuView menuView;

        private readonly IAddSkaterView addSkaterView;

        private readonly IListSkaterView listSkaterView;

        private Dictionary<ConsoleKey, Action> optionActionDictionary;

        public AppMain(IAddSkaterView addSkaterView, 
            IListSkaterView listSkaterView, 
            IMenuView menuView)
        {
            this.addSkaterView = addSkaterView;
            this.listSkaterView = listSkaterView;
            this.menuView = menuView;

            InitOptionAction();
        }

        public void Run()
        {
            DisplayMenu();
        }

        private void InitOptionAction()
        {
            optionActionDictionary = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.D1, addSkaterView.Add},
                {ConsoleKey.D2, listSkaterView.List}
            };
        }

        private void DisplayMenu()
        {
            menuView.DisplayMenu();

            Console.WriteLine(ViewResource.EscapeExitInfo);
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;

                if (!optionActionDictionary.ContainsKey(key)) continue;

                optionActionDictionary[key]();
                menuView.DisplayMenu();

            } while (key != ConsoleKey.Escape);
        }
    }
}
