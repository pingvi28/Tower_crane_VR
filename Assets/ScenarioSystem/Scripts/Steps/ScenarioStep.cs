using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Every scenario made from steps. Steps should be launched and stopped at their lifecycle. After end of process
    /// task should report to the launcher. Then it stops before launching next task.
    /// </summary>
    public abstract class ScenarioStep : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Always return false when not updatable</returns>
        public abstract bool IsLaunched();
        
        /// <summary>
        /// Initiate operations for this step.
        /// </summary>
        /// <param name="launcher">Reference to the calling launcher</param>
        public abstract void Launch(IStepLauncher launcher);

        /// <summary>
        /// Finalize operations of this step.
        /// </summary>
        /// <param name="launcher">Reference to the calling launcher</param>
        public abstract void Stop(IStepLauncher launcher);
    }
}