using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Plays once selected audio clip on specified source and passes to next step.
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create LaunchSubscenario",
        fileName = "Launch Subscenario Step",
        order = 0)]
    public class LaunchSubscenario : ScenarioStep
    {
        [SerializeField] private string subplayerName;
        [SerializeField] private ScenarioScript scenario;

        private ScenarioPlayer _subplayer;

        public override bool IsLaunched()
        {
            return false;
        }

        public override void Launch(IStepLauncher launcher)
        {
#if DEBUG_SCENARIO
            Debug.Log("<color=green>Launching</color> " + name + " step");
#endif
            _subplayer = launcher.GetResources().GetGameObject(subplayerName).GetComponent<ScenarioPlayer>();
            _subplayer.StartScenario(scenario);

            launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            _subplayer.StopScenario(true);

#if DEBUG_SCENARIO
            Debug.Log(name + " <color=red>step ended</color>");
#endif
        }
    }
}