//using CombatFirstAidVR.Scripts;
//using ScenarioSystem.Scripts.Interfaces;
//using Unity.VisualScripting;
//using UnityEngine;

//namespace ScenarioSystem.Scripts.Steps
//{
//    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create StayInArea", fileName = "StayInArea Step",
//        order = 0)]
//    public class StayInArea : ScenarioStep
//    {
//        [SerializeField] private string targetAreaName;
//        [SerializeField] private string targetObjectName;

//        [SerializeField] private float requiredResidenceTime; 
//        private float startTime;

//        private bool _isLaunched;
//        [SerializeField] private bool inArea;
        
//        private IStepLauncher _launcher;

//        private ScenarioStayArea gazeArea;

//        void Update()
//        {
//            if (inArea == true)
//            {
//                if (Time.time > startTime + requiredResidenceTime)
//                {
//                    End();
//                }
//            }

//        }

//        public override bool IsLaunched()
//        {
//            return _isLaunched;
//        }

//        public override void Launch(IStepLauncher launcher)
//        {
//            _launcher = launcher;

//            inArea = false;
//            gazeArea = _launcher.GetResources().GetGameObject(targetAreaName).GetComponent<ScenarioStayArea>();
//            gazeArea.EnteringZone += OnEnteringZone;
//            gazeArea.ExitingZone += OnExetingZone;
//            gazeArea.Collider =  _launcher.GetResources().GetGameObject(targetObjectName).GetComponent<Collider>();

//            launcher.UpdatedStep += Update;

//            _isLaunched = true;
//        }
        
//        private void OnEnteringZone()
//        {
//            startTime = Time.time;
//            inArea = true;
//        }
        
//        private void OnExetingZone()
//        {
//            inArea = false;
//        }

//        private void End()
//        {
//            gazeArea.EnteringZone -= OnEnteringZone;
//            gazeArea.ExitingZone -= OnExetingZone;
//            _launcher.StepFinished(this);
//        }
        
//        public override void Stop(IStepLauncher launcher)
//        {
//            _isLaunched = false;
//        }
//    }
//}