using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Waits for specified time and then passes to the next step
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create PlayStepIfBool", fileName = "Play Step If Bool",
        order = 0)]
    internal class PlayStepIfBool : ScenarioStep, IStepLauncher
    {
        [SerializeField] private string boolName;
        [SerializeField] private ScenarioStep stepToPlayIfTrue;
        private ScenarioStep _stepInstance;

        private IStepLauncher _launcher;

        public override bool IsLaunched()
        {
            return true;
        }

        public override void Launch(IStepLauncher launcher)
        {
#if DEBUG_SCENARIO
            Debug.Log("<color=green>Launching</color> " + name + " step");
#endif
            _launcher = launcher;

            if (launcher.GetResources().GetBool(boolName))
            {
                _stepInstance = Instantiate(stepToPlayIfTrue);
                _stepInstance.Launch(this);

                if (!_stepInstance.IsLaunched())
                {
                    _launcher.StepFinished(this);
                }
            }
            else
            {
                _launcher.StepFinished(this);
            }

            Debug.Log("<color=green>Launched</color> " + name);
        }

        public override void Stop(IStepLauncher launcher)
        {
            if (_stepInstance.IsLaunched())
            {
                _stepInstance.Stop(this);
            }

#if DEBUG_SCENARIO
            Debug.Log(name + " <color=red>step ended</color>");
#endif
        }

        public void StepFinished(ScenarioStep step)
        {
            if (step.IsLaunched())
            {
                step.Stop(this);
                _launcher.StepFinished(this);
            }
            else
            {
                _launcher.StepFinished(this);
            }
        }

        public IResourceManager GetResources()
        {
            return _launcher.GetResources();
        }

        public UpdateDelegate UpdatedStep
        {
            get => _launcher.UpdatedStep;
            set => _launcher.UpdatedStep = value;
        }
    }
}