using System.Collections.Generic;
using UnityEngine;

namespace ScenarioSystem.Scripts
{

    [System.Serializable]
    public class GameObjectsLinks
    {
        public string name;
        public GameObject gameObject;
    }

    /*[System.Serializable]
    public class GameObjectsLinksList : List<GameObjectsLinks>
    {
    }*/

    [System.Serializable]
    public class AudioSourcesLinks
    {
        public string name;
        public AudioSource audioSource;
    }

    /*[System.Serializable]
    public class AudioSourcesLinksList : List<AudioSourcesLinks>
    {
    }*/

    [System.Serializable]
    public class Booleans
    {
        public string boolName;
        public bool value;
    }

    /*[System.Serializable]
    public class BooleansList : List<Booleans>
    {
    }*/
}