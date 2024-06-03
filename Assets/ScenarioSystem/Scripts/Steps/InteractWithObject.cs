using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Interact With Object", fileName = "Interact With Object", order = 0)]
    public class InteractWithObject : ScenarioStep
    {
        [SerializeField] private string targetGameObjectName;
    
        private IStepLauncher _launcher;
    
        private bool _isLaunched;

        private ScenarioInteractiveObject scenarioInteractiveObject;
        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
            _launcher = launcher;
            scenarioInteractiveObject = _launcher.GetResources().GetGameObject(targetGameObjectName)
                .GetComponent<ScenarioInteractiveObject>();

            scenarioInteractiveObject.ObjectActivated += InteractObject;

            _isLaunched = true;
        }

        private void InteractObject()
        {
            _launcher.StepFinished(this);
            scenarioInteractiveObject.ObjectActivated -= InteractObject;
        }
    
        public override void Stop(IStepLauncher launcher)
        {
        
        }
    }
}
