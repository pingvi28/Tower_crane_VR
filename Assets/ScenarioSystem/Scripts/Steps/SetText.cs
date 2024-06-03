using System.Collections.Generic;
using ScenarioSystem.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create SetText", fileName = "SetText Step", order = 0)]
    public class SetText : ScenarioStep
    {
        [SerializeField] private List<string> targetObjectNames;
        [SerializeField] private List<string> newTexts;

        [SerializeField] private List<TextMeshProUGUI> texts;
    
        private IStepLauncher _launcher;
    
        private bool _isLaunched;
    
        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
            _launcher = launcher;
                
            for (int i = 0; i < targetObjectNames.Count; i++)
            {
                texts[i] = _launcher.GetResources().GetGameObject(targetObjectNames[i]).GetComponent<TextMeshProUGUI>();
                texts[i].text = newTexts[i];
            }
        
            launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            _isLaunched = false;
        }
    }
}
