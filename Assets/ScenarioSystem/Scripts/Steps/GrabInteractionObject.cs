using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Grab Interactive Object", fileName = "Grab Interactive Object", order = 0)]
    public class GrabInteractionObject : ScenarioStep
    {
        [SerializeField] private string targetGameObjectName;

        private IStepLauncher _launcher;

        private bool _isLaunched;

        private ScenarioGrabbableInteractiveObject scenarioGrabbableInteractiveObject;

        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
            _launcher = launcher;
            scenarioGrabbableInteractiveObject = _launcher.GetResources().GetGameObject(targetGameObjectName)
                .GetComponent<ScenarioGrabbableInteractiveObject>();

            scenarioGrabbableInteractiveObject.GrabbableObjectActivated += InteractObject;

            _isLaunched = true;
        }

        private void InteractObject()
        {
            _launcher.StepFinished(this);
            scenarioGrabbableInteractiveObject.GrabbableObjectActivated -= InteractObject;
        }

        public override void Stop(IStepLauncher launcher)
        {

        }
    }
}
