using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Waits for specified time and then passes to the next step
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create Delay", fileName = "Delay", order = 0)]
    public class Delay : ScenarioStep
    {
        [SerializeField] private float delay;
        private IStepLauncher _launcher;
        private float _startTime;
        private bool _isLaunched;

        private void Update()
        {
            if (Time.time > _startTime + delay)
            {
                _launcher.StepFinished(this);
            }
        }

        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
#if DEBUG_SCENARIO
            Debug.Log("<color=green>Launching</color> " + name + " step");
#endif
            _launcher = launcher;
            _startTime = Time.time;
            _launcher.UpdatedStep = Update;
            _isLaunched = true;
        }

        public override void Stop(IStepLauncher launcher)
        {
            if (launcher.UpdatedStep == Update)
            {
                launcher.UpdatedStep = null;
            }

            _isLaunched = false;

#if DEBUG_SCENARIO
            Debug.Log(name + " <color=red>step ended</color>");
#endif
        }
    }
}