using ScenarioSystem.Scripts.Interfaces;
using ScenarioSystem.Scripts.UI;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create SetMenuSound", fileName = "Set Menu Sound Step",
        order = 0)]
    internal class SetMenuSound : ScenarioStep
    {
        [Tooltip("Audio that should be played when menu appears")] [SerializeField]
        private AudioClip clip;

        public override bool IsLaunched()
        {
            return false;
        }

        public override void Launch(IStepLauncher launcher)
        {
#if DEBUG_SCENARIO
Debug.Log("<color=green>Launching</color> " + name + " step");
#endif

            launcher.GetResources().GetGameObject("Menu").GetComponent<MenuController>().OpenSound = clip;

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