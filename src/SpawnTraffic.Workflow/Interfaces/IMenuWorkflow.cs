using SpawnTraffic.Common.Domains;
using SpawnTraffic.Model;

namespace SpawnTraffic.Workflow.Interfaces
{
    public interface IMenuWorkflow
    {
        Result<Menu> GetMenu();
    }
}