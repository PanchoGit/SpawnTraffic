using System.Collections.Generic;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Model;

namespace SpawnTraffic.Workflow.Interfaces
{
    public interface ISkaterWorkflow
    {
        Result<SkaterModel> Add(AddSkaterModel model);

        Result<List<SkaterModel>> Get();
    }
}