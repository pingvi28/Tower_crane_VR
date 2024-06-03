using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New GameData", menuName = "GaME Data", order = 1)]
public class GameInfo : ScriptableObject
{
    public int[] buildingHeight;
    public bool fogEnabled;
    public float fogDensity;
    
    public bool rainEnabled;
    public float rainDensity;

    public int[] getBuildingHeight()
    {
        return buildingHeight;
    }
    
    public void setUserBuildingHeight(int newBuildingHeight)
    {
        buildingHeight[^1] = newBuildingHeight;
    }
    
    public bool getFogEnabled()
    {
        return fogEnabled;
    }
    
    public void setFogEnabled(bool newFogEnabled)
    {
        fogEnabled = newFogEnabled;
    }
    
    public float getFogDensity()
    {
        return fogDensity;
    }
    
    public void setFogDensity(float newFogDensity)
    {
        fogDensity = newFogDensity;
    }
    
    public bool getRainEnabled()
    {
        return rainEnabled;
    }
    
    public void setRainEnabled(bool newRainEnabled)
    {
        rainEnabled = newRainEnabled;
    }
    
    public float getRainDensity()
    {
        return rainDensity;
    }
    
    public void setRainDensity(float newRainDensity)
    {
        rainDensity = newRainDensity;
    }
}
