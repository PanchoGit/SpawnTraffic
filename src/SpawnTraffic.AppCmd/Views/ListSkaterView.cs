using System;
using System.Collections.Generic;
using Autofac;
using SpawnTraffic.AppCmd.Helpers;
using SpawnTraffic.AppCmd.Resources;
using SpawnTraffic.AppCmd.Views.Interfaces;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow.Interfaces;

namespace SpawnTraffic.AppCmd.Views
{
    public class ListSkaterView : IListSkaterView
    {
        private readonly ILifetimeScope lifetimeScope;

        public ListSkaterView(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public void List()
        {
            Console.WriteLine(ViewResource.ListSkaterTitle);

            var result = Get();

            foreach (var item in result.Data)
            {
                Console.WriteLine(ViewResource.ListSkaterResult, item.Id, item.Name, item.Brand);
            }

            Console.WriteLine(ViewResource.ListSkaterTitle + Environment.NewLine);

            MessageDisplayHelper.DisplayMessage(result);
        }

        private Result<List<SkaterModel>> Get()
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var workflow = scope.Resolve<ISkaterWorkflow>();

                return workflow.Get();
            }
        }
    }
}
