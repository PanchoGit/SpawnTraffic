using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.DataCache.Interfaces;
using SpawnTraffic.Logger;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow.Interfaces;
using SpawnTraffic.Workflow.Resources;

namespace SpawnTraffic.Workflow
{
    public class SkaterWorkflow : ISkaterWorkflow
    {
        private readonly ILoggerManager logger;

        private readonly ISkaterData skaterData;

        private readonly IMapper mapper;

        public SkaterWorkflow(ILoggerManager logger, 
            ISkaterData skaterData, IMapper mapper)
        {
            this.logger = logger;
            this.skaterData = skaterData;
            this.mapper = mapper;
            InitSkaterModels();
        }

        public Result<SkaterModel> Add(AddSkaterModel model)
        {
            var result = new Result<SkaterModel>();

            try
            {
                var data = Translate(model);

                var skaters = skaterData.Get();

                skaters.Add(data);

                skaterData.Set(skaters);

                result.Data = data;

                result.AddMessages(logger
                    .LogSuccess(string.Format(WorkflowResource.SkaterAdded, 
                        JsonConvert.SerializeObject(model))));
            }
            catch (Exception ex)
            {
                result.AddMessages(logger.LogError(ex.Message));
            }

            return result;
        }

        public Result<List<SkaterModel>> Get()
        {
            var result = new Result<List<SkaterModel>>();

            try
            {
                result.Data = skaterData.Get();

                result.AddMessages(logger.LogSuccess(WorkflowResource.SkaterList));
            }
            catch (Exception ex)
            {
                result.AddMessages(logger.LogError(ex.Message));
            }

            return result;
        }

        private void InitSkaterModels()
        {
            if(skaterData.Get().Any()) return;

            var skaters = new List<SkaterModel>
            {
                new SkaterModel
                {
                    Id = Guid.NewGuid(), Name = "Luan Oliveira", Brand = "Flip", Created = DateTimeOffset.UtcNow
                },
                new SkaterModel
                {
                    Id = Guid.NewGuid(), Name = "Paul Rodriguez", Brand = "Primitive", Created = DateTimeOffset.UtcNow
                }
            };

            skaterData.Set(skaters);
        }

        private SkaterModel Translate(AddSkaterModel model)
        {
            return mapper.Map<AddSkaterModel, SkaterModel>(model);
        }
    }
}
