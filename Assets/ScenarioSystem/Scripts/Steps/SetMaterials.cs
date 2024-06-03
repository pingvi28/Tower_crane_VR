using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create SetMaterials", fileName = "Set Materials Step",
        order = 0)]
    internal class SetMaterials : ScenarioStep
    {
        [SerializeField] private string targetObjectName;
        [SerializeField] private Material[] materials;


        public override bool IsLaunched()
        {
            return false;
        }

        public override void Launch(IStepLauncher launcher)
        {
            #if DEBUG_SCENARIO
Debug.Log("<color=green>Launching</color> " + name + " step");
#endif
            var renderer = launcher.GetResources().GetGameObject(targetObjectName).GetComponent<MeshRenderer>();
            renderer.materials = materials;

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