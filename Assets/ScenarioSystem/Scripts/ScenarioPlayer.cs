using System;
using System.Collections.Generic;
using System.Linq;
using ScenarioSystem.Scripts.Interfaces;
using ScenarioSystem.Scripts.Steps;
using UnityEngine;

namespace ScenarioSystem.Scripts
{
    public class ScenarioPlayer : MonoBehaviour, IStepLauncher, IResourceManager
    {
        [Header("Scenarios")] [SerializeField] private ScenarioScript scenario;

        [Header("Resource Manager")] [SerializeField]
        protected ScenarioPlayer surrogateResourceManager;

        public List<AudioSourcesLinks> audioSourcesLinks;
         
        public List<GameObjectsLinks> gameObjectsLinks;

        public List<Booleans> booleans;

        public delegate void ScenarioEndedDelegate(bool forcedOrEarlyStop, ScenarioScript scenarioScript);

        public event ScenarioEndedDelegate ScenarioEnded;

        private int _currentScenarioStep = -1;
        private bool _ignoreEndedSteps;

        protected virtual void Awake()
        {
            if (scenario != null)
            {
                scenario = Instantiate(scenario);
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            UpdatedStep?.Invoke();
        }

        public void StartScenario(ScenarioScript newScenario)
        {
            if (scenario != null)
            {
                StopScenario(true);
            }

            _ignoreEndedSteps = false;
            scenario = newScenario;

#if DEBUG_SCENARIO
            Debug.Log("Starting " + scenario.name + " scenario!");
#endif
            _currentScenarioStep = 0;
            SwitchStepTo(null, scenario.steps[_currentScenarioStep]);
        }


        /// <summary>
        /// Immediately stops scenario at current step.
        /// </summary>
        /// <param name="forcedOrEarlyStop"></param>
        public void StopScenario(bool forcedOrEarlyStop)
        {
            // Do nothing if already stopped
            if (_currentScenarioStep < 0 || !scenario) return;


#if DEBUG_SCENARIO
            Debug.Log("Stopping " + scenario.name + " scenario!");
#endif

            // Else Stop current step and reset
            if (_currentScenarioStep < scenario.steps.Count)
            {
                var step = scenario.steps[_currentScenarioStep];

                if (step.IsLaunched())
                {
                    step.Stop(this);
                }
            }

            _currentScenarioStep = -1;

            _ignoreEndedSteps = true;
            foreach (var finalizingStep in scenario.finalizingSteps)
            {
                finalizingStep.Launch(this);
            }

            OnScenarioEnded(forcedOrEarlyStop, scenario);
        }

        protected void SwitchStepTo(ScenarioStep previous, ScenarioStep next)
        {
            if (previous != null)
            {
                previous.Stop(this);
            }

            next.Launch(this);
        }

        protected void StartNextStep()
        {
            var previousStep = scenario.steps[_currentScenarioStep];
            _currentScenarioStep++;

            if (_currentScenarioStep < scenario.steps.Count)
            {
                var nextStep = scenario.steps[_currentScenarioStep];
                SwitchStepTo(previousStep, nextStep);
            }
            else
            {
                previousStep.Stop(this);
                StopScenario(false);
            }
        }

        private void OnScenarioEnded(bool forcedOrEarlyStop, ScenarioScript scenarioScript)
        {
            ScenarioEnded?.Invoke(forcedOrEarlyStop, scenarioScript);
        }

        #region IStepLauncher

        public void StepFinished(ScenarioStep step)
        {
            if (_ignoreEndedSteps) return;

            if (scenario.steps[_currentScenarioStep] != step)
            {
                throw new Exception("Wrong step tried to tell that it finished!" +
                                    "\nCurrent step: " +
                                    scenario.steps[_currentScenarioStep].name +
                                    "\nFinished step: " + step.name);
            }

            StartNextStep();
        }

        public IResourceManager GetResources()
        {
            return surrogateResourceManager ? surrogateResourceManager : this;
        }

        public UpdateDelegate UpdatedStep { get; set; }

        #endregion

        #region IResourceManager

        public AudioSource GetAudioSource(string sourceName)
        {
            return audioSourcesLinks.Single(links => links.name.Equals(sourceName)).audioSource;
        }

        public GameObject GetGameObject(string gameObjectName)
        {
            return gameObjectsLinks.ToList().Find(links => links.name.Equals(gameObjectName)).gameObject;
        }

        public GameObject[] GetGameObjects(IEnumerable<string> gameObjectName)
        {
            return (from gameObjectsLink in gameObjectsLinks
                where gameObjectName.Any(linkName => gameObjectsLink.name.Equals(linkName))
                select gameObjectsLink.gameObject).ToArray();
        }

        public bool GetBool(string boolName)
        {
            return booleans.Single(bools => bools.boolName.Equals(boolName)).value;
        }

        public void SetBool(string boolName, bool value)
        {
            booleans.Single(bools => bools.boolName.Equals(boolName)).value = value;
        }

        #endregion
    }
}