using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptControll : MonoBehaviour
{
    [SerializeField]private GameInfo gameInfo;
    
    [SerializeField]private Slider height_slider;
    [SerializeField]private Slider fog_slider;
    
    [SerializeField]private Toggle rain_toggle;
    
    [SerializeField]private Toggle fog_toggle;
    [SerializeField]private GameObject density_slider_gr;

    [SerializeField]private TextMeshProUGUI count_height;
    [SerializeField]private TextMeshProUGUI count_fog;

    public void Start()
    {
        gameInfo.setFogEnabled(fog_toggle.isOn);
        gameInfo.setRainEnabled(rain_toggle.isOn);
    }

    public void Height()
    {
        gameInfo.setUserBuildingHeight(int.Parse(height_slider.value.ToString()));
    }
    
    public void Fog()
    {
        bool isOnFog = fog_toggle.isOn;
        gameInfo.setFogEnabled(isOnFog);

        density_slider_gr.SetActive(isOnFog);
    }
    
    public void Rain()
    {
        gameInfo.setRainEnabled(rain_toggle.isOn);
    }
    
    public void FogSlider()
    {
        gameInfo.setFogDensity(int.Parse(fog_slider.value.ToString()) * 0.005f);
    }

    public void Update()
    {
        count_height.text = height_slider.value.ToString();
        count_fog.text = fog_slider.value.ToString();
    }
}
