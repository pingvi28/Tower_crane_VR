using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Waits for specified time and then passes to the next step
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create SetMaterialParamByCurve",
        fileName = "Set Material Param By Curve step", order = 0)]
    internal class SetMaterialParamByCurve : ScenarioStep
    {
        [SerializeField] private string targetName;
        [SerializeField] private float duration;
        [SerializeField] private string floatParameter;
        [SerializeField] private bool useSourceMaterial;
        [SerializeField] private AnimationCurve curve;

        private IStepLauncher _launcher;
        private float _startTime;
        private bool _isLaunched;
        private Material _material;
        private float _reverseDuration;

        private void Update()
        {
            var currentTime = Time.time - _startTime;

            if (currentTime > duration)
            {
                _launcher.StepFinished(this);
                _material.SetFloat(floatParameter, curve.Evaluate(1f));
            }
            else
            {
                _material.SetFloat(floatParameter, curve.Evaluate(currentTime * _reverseDuration));
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
            var meshRenderer = _launcher.GetResources().GetGameObject(targetName).GetComponent<MeshRenderer>();
            _material = useSourceMaterial ? meshRenderer.sharedMaterial : meshRenderer.material;
            _reverseDuration = 1f / duration;

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