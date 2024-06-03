using System;
using UnityEngine;

namespace ScenarioSystem.Scripts
{
    public class SetStateByScenario : MonoBehaviour
    {
        [SerializeField] private EnableGroup[] enableGroups;

        public void EnableGameObjects(int index)
        {
            foreach (var go in enableGroups[index].enabled)
            {
                go.SetActive(true);
            }

            foreach (var go in enableGroups[index].disabled)
            {
                go.SetActive(false);
            }
        }

        [ContextMenu("Enable0")]
        public void Enable0()
        {
            EnableGameObjects(0);
        }

        [ContextMenu("Enable1")]
        public void Enable1()
        {
            EnableGameObjects(1);
        }

        [ContextMenu("Enable2")]
        public void Enable2()
        {
            EnableGameObjects(2);
        }

        [ContextMenu("Enable All")]
        public void EnableAll()
        {
            var children = transform.GetComponentInChildren<Transform>();
            foreach (Transform child in children)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    [Serializable]
    public struct EnableGroup
    {
        public string name;
        public GameObject[] enabled;
        public GameObject[] disabled;
    }
}