//using ScenarioSystem.Scripts.Interfaces;
//using UnityEngine;

//namespace ScenarioSystem.Scripts.Steps
//{
//    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create PlaceItems", fileName = "PlaceItems Step",
//        order = 0)]
//    public class PlaceItems : ScenarioStep
//    {
//        [SerializeField] private string targetDesiredLocation;
        
//        [SerializeField] private string[] targetItemNames;

//        private bool _isLaunched;
//        private IStepLauncher _launcher;

//        public override bool IsLaunched()
//        {
//            return _isLaunched;
//        }

//        public override void Launch(IStepLauncher launcher)
//        {
//#if DEBUG_SCENARIO
//            Debug.Log("<color=green>Launching</color> " + name + " step");
//#endif
//            _launcher = launcher;

//            _launcher.GetResources().GetGameObject(targetDesiredLocation).GetComponent<DesiredLocation>().ObjectDelivered += OnObjectDelivered;

//            var objects = new GrabbableInteractiveObject[targetItemNames.Length];
            
//            for (int i = 0; i < targetItemNames.Length; i++)
//            {
                
//                objects[i] =  _launcher.GetResources().GetGameObject(targetItemNames[i]).GetComponent<GrabbableInteractiveObject>();
//            }

//            _launcher.GetResources().GetGameObject(targetDesiredLocation).GetComponent<DesiredLocation>()
//                .RequiredObjects.AddRange(objects);


//                _isLaunched = true;
//        }

//        private void OnObjectDelivered()
//        {
//            _launcher.GetResources().GetGameObject(targetDesiredLocation).GetComponent<DesiredLocation>().ObjectDelivered -=
//                OnObjectDelivered;
//            _launcher.StepFinished(this);
//        }
        

//        public override void Stop(IStepLauncher launcher)
//        {

//            _isLaunched = false;

//#if DEBUG_SCENARIO
//            Debug.Log(name + " <color=red>step ended</color>");
//#endif
//        }
//    }
//}
