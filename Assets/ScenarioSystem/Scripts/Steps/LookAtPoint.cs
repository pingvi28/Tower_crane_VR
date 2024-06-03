using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create LookAtPoint", fileName = "LookAtPoint Step",
        order = 0)]
    public class LookAtPoint : ScenarioStep
    {
        [SerializeField] private string targetAreaName;
    
        [SerializeField] private float requiredGazeTime;
        private float gazeCooldown;
        [SerializeField] private float gazeDistance;
    
        [SerializeField] private Collider gazeArea;

        [SerializeField] private Camera playerCamera;
    
    
        private bool _isLaunched;
        private IStepLauncher _launcher;

    
        void Update()
        {
            if(Physics.Raycast(playerCamera.ViewportPointToRay((new Vector3(0.5f, 0.5f, 0))), out var hit, gazeDistance))
            {
                if (hit.collider == gazeArea)
                {
                    if (requiredGazeTime > 0)
                    {
                        requiredGazeTime -= Time.deltaTime;
                    }
                    else
                    {
                        _launcher.StepFinished(this);
                    }
                }
                else
                {
                    requiredGazeTime = gazeCooldown;
                }
            }
        }

        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
            _launcher = launcher;

            gazeArea = _launcher.GetResources().GetGameObject(targetAreaName).GetComponent<Collider>();
        
            playerCamera = Camera.main;

            gazeCooldown = requiredGazeTime;
        
            launcher.UpdatedStep += Update;

            _isLaunched = true;
        }

        public override void Stop(IStepLauncher launcher)
        {
            _isLaunched = false;
        }
    }
}
