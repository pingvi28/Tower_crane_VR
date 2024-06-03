using UnityEngine;

namespace ScenarioSystem.Scripts
{
    public sealed class ScenariosPlayer : ScenarioPlayer
    {
        [Header("Scenarios")] [SerializeField] private ScenarioScript[] scenarios;

        private int _currentScenarioIndex = -1;

        protected override void Awake()
        {
            for (var i = 0; i < scenarios.Length; i++)
            {
                scenarios[i] = Instantiate(scenarios[i]);
                StartScenario(i);
            }
        }
        
        public void StartScenario(int index)
        {
            _currentScenarioIndex = index;
            StartScenario(scenarios[_currentScenarioIndex]);
        }
    }
}