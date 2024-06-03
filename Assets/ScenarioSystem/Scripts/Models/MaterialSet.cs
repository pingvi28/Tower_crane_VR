using System;
using UnityEngine;

namespace ScenarioSystem.Scripts.Models
{
    public class MaterialSet : MonoBehaviour
    {
        [SerializeField] private SetUnit[] set;

        public virtual void Set(int index)
        {
            var setUnit = set[index];

            foreach (var rendererSet in setUnit.renderers)
            {
                try
                {
                    rendererSet.renderer.materials = rendererSet.materials;
                }
                catch
                {
                    Debug.Log("renderer is null: " + rendererSet.renderer);
                }
            }
        }
    }

    [Serializable]
    public struct SetUnit
    {
        public string name;
        public RenderersSet[] renderers;
    }

    [Serializable]
    public struct RenderersSet
    {
        public MeshRenderer renderer;
        public Material[] materials;
    }
}