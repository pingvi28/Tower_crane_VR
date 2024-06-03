using System.Collections.Generic;
using ScenarioSystem.Scripts.Interfaces;
using UnityEngine;

namespace ScenarioSystem.Scripts.Steps
{
    /// <summary>
    /// Plays once selected audio clip on specified source and passes to next step.
    /// </summary>
    [CreateAssetMenu(menuName = "ScenarioSystem/Steps/Create PlayAudioClip", fileName = "Play Audio Clip Step",
        order = 0)]
    public class PlayAudioClip : ScenarioStep
    {
        [SerializeField] private List<string> audioSourceNames;
        [SerializeField] private List<AudioClip> clipsToPlay;

        protected List<AudioSource> audioSources;
        
        protected IStepLauncher Launcher;
        protected bool _isLaunched;

        public override bool IsLaunched()
        {
            return _isLaunched;
        }

        public override void Launch(IStepLauncher launcher)
        {
#if DEBUG_SCENARIO
            Debug.Log("<color=green>Launching</color> " + name + " step");
#endif


            Launcher = launcher;
            Launcher.UpdatedStep = Update;
            for (int i = 0; i < audioSourceNames.Count; i++)
            {
                audioSources[i] = Launcher.GetResources().GetAudioSource(audioSourceNames[i]);
                audioSources[i].clip = clipsToPlay[i];
                audioSources[i].loop = false;
                audioSources[i].Play();
            }
            _isLaunched = true;

        }

        protected virtual void Update()
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i].isPlaying)
                {
                    return;
                }
            }
            Launcher.StepFinished(this);
        }

        public override void Stop(IStepLauncher launcher)
        {
            if (launcher.UpdatedStep == Update)
            {
                for (int i = 0; i < audioSources.Count; i++)
                {
                    audioSources[i].Stop();
                }
                launcher.UpdatedStep = null;
            }

            _isLaunched = false;

#if DEBUG_SCENARIO
            Debug.Log(name + " <color=red>step ended</color>");
#endif
        }
    }
}