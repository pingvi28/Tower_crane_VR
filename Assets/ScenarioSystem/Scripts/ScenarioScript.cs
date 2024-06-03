using System.Collections.Generic;
using ScenarioSystem.Scripts.Steps;
using UnityEngine;

namespace ScenarioSystem.Scripts
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Create ScenarioScript", fileName = "ScenarioScript", order = 0)]
    public class ScenarioScript : ScriptableObject
    {
        [SerializeField] internal List<ScenarioStep> steps; 
        [SerializeField] internal List<ScenarioStep> finalizingSteps;
    }

    /*[System.Serializable]
    public class StepsList : List<ScenarioStep>
    {
    }*/
}