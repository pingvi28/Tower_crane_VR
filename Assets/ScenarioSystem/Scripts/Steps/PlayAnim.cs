using System.Collections.Generic;
using ScenarioSystem.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Plays once selected animation on specified gameobject and passes to next step.
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create PlayAnim", fileName = "Play Anim Step",
        order = 0)]
    public class PlayAnim : ScenarioStep
    {
        [SerializeField] private List<string> targetGameObjectNames;
        [SerializeField] private List<AnimationClip> clipsToPlay;

        /// <summary>
        /// Cached Animation component of target
        /// </summary>
        private List<Animation> _animations;
        private IStepLauncher _launcher;
        private bool _isLaunched;

        private Animator anim;

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
            _launcher.UpdatedStep = Update;
            for (int i = 0; i >= targetGameObjectNames.Count; i++)
            {
                _animations[i] = _launcher.GetResources().GetGameObject(targetGameObjectNames[i]).GetComponent<Animation>();
                _animations[i].clip = clipsToPlay[i];
                _animations[i].Play();
            }

            

            _isLaunched = true;
        }

        private void Update()
        {
            for (int i = 0; i < _animations.Count; i++)
            {
                if (_animations[i].isPlaying && _isLaunched)
                {
                    return;                    
                }
            }
            _launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            if (launcher.UpdatedStep == Update)
            {
                launcher.UpdatedStep = null;
            }

            for (int i = 0; i < _animations.Count; i++)
            {
                _animations[i].Stop();
            }

            _isLaunched = false;

#if DEBUG_SCENARIO
            Debug.Log(name + " <color=red>step ended</color>");
#endif
        }
    }
}