using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem.Scripts.Interfaces
{
    public interface IResourceManager
    {
        AudioSource GetAudioSource(string sourceName);
        GameObject GetGameObject(string gameObjectName);
        GameObject[] GetGameObjects(IEnumerable<string> gameObjectName);
        bool GetBool(string boolName);
        void SetBool(string boolName, bool value);
    }
}