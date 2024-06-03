using ScenarioSystem.Scripts.Steps;

namespace ScenarioSystem.Scripts.Interfaces
{
    public delegate void UpdateDelegate();
    public interface IStepLauncher
    {
        void StepFinished(ScenarioStep step);
        IResourceManager GetResources();
        UpdateDelegate UpdatedStep { get; set; }
    }
}