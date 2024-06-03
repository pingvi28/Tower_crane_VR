using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create EnableObjects", fileName = "EnableObjects Step",
        order = 0)]
    public class EnableObjects : ScenarioStep
    {
        [SerializeField] private string[] objectsToEnable;
        public override bool IsLaunched()
        {
            return false;
        }

        public override void Launch(IStepLauncher launcher)
        {
            var gameObjects = launcher.GetResources().GetGameObjects(objectsToEnable);

            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(true);
            }
            
            launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            
        }
    }
}
