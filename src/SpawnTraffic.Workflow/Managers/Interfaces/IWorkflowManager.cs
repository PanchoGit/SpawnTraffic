using System;

namespace SpawnTraffic.Workflow.Managers.Interfaces
{
    public interface IWorkflowManager
    {
        void Execute(Action action);
    }
}