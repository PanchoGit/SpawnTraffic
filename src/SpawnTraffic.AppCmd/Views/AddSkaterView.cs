using System;
using Autofac;
using SpawnTraffic.AppCmd.Helpers;
using SpawnTraffic.AppCmd.Resources;
using SpawnTraffic.AppCmd.Views.Interfaces;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow.Interfaces;

namespace SpawnTraffic.AppCmd.Views
{
    public class AddSkaterView : IAddSkaterView
    {
        private readonly ILifetimeScope lifetimeScope;

        public AddSkaterView(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public void Add()
        {
            Console.WriteLine(ViewResource.AddSkaterTitle);
            Console.WriteLine(ViewResource.AddSkaterInputName);
            var skaterName = Console.ReadLine();
            Console.WriteLine(ViewResource.AddSkaterInputBrand);
            var skaterBrand = Console.ReadLine();

            var model = new AddSkaterModel
            {
                Name = skaterName,
                Brand = skaterBrand
            };

            var result = AddSkater(model);

            Console.WriteLine();

            MessageDisplayHelper.DisplayMessage(result);
        }

        private Result<SkaterModel> AddSkater(AddSkaterModel model)
        {
            using (var scope = lifetimeScope.BeginLifetimeScope())
            {
                var workflow = scope.Resolve<ISkaterWorkflow>();

                return workflow.Add(model);
            }
        }
    }
}
