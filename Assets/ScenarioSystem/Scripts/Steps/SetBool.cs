using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create SetBool", fileName = "Set Bool Step", order = 0)]
    public class SetBool : ScenarioStep
    {
        [SerializeField] private string boolName;
        [SerializeField] private bool value;

        public override bool IsLaunched()
        {
            return false;
        }

        public override void Launch(IStepLauncher launcher)
        {
            #if DEBUG_SCENARIO
Debug.Log("<color=green>Launching</color> " + name + " step");
#endif
            launcher.GetResources().SetBool(boolName, value);
            
            launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            #if DEBUG_SCENARIO
Debug.Log(name + " <color=red>step ended</color>");
#endif
        }
    }
}