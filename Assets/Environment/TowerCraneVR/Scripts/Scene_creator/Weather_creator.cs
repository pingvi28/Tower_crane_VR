using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Serialization;

public class Weather_creator : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private GameObject rain;

    void Start()
    {
        RenderSettings.fog = gameInfo.getFogEnabled();
        if (gameInfo.getFogEnabled())
        {
            RenderSettings.fogDensity=gameInfo.getFogDensity();
            RenderSettings.skybox = null;
        }

        if (gameInfo.getRainEnabled())
        {
            rain.SetActive(true);
            ParticleSystem rain_system = rain.GetComponent<ParticleSystem>();
            rain_system.Stop();

            var particleMainSys = rain_system.main;
            particleMainSys.startLifetime = 3f;
            
            rain_system.Play();
        }

        //camera.transform.position = GameObject.FindWithTag("personCrane").transform.position;
    }
}

