using System;
using UnityEngine;

namespace ScenarioSystem.Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private float menuDistance = 1.5f;
        [SerializeField] private float menuHeight = 1.5f;

        [SerializeField] private Transform placementAndLookTarget;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private Vector2 touchZone;


        public AudioClip OpenSound
        {
            set => openSound = value;
        }


        // private List<TouchState> touchedUiElementsStates = new List<TouchState>();
        private void OnEnable()
        {
            PlaceMenu(placementAndLookTarget);
        }

        private void PlaceMenu(Transform target)
        {
            var menuPosition = target.position + target.forward * menuDistance;
            menuPosition.y = menuHeight;
            transform.position = menuPosition;
        }

        // private void RaycasterOnRayTouchedUI(RaycastResult raycastResult)
        // {
        //     var touchStateIndex = touchedUiElementsStates.FindIndex(state => state.UiElement == raycastResult.HitUiElement);
        //
        //     if (touchStateIndex < 0)
        //     {
        //         touchedUiElementsStates.Add(new TouchState()
        //             {UiElement = raycastResult.HitUiElement, WasHoveredThisFrame = true});
        //         raycastResult.HitUiElement.OnHover();
        //     }
        //     else
        //     {
        //         var touchedUiElement = touchedUiElementsStates[touchStateIndex];
        //         touchedUiElement.WasHoveredThisFrame = true;
        //         touchedUiElementsStates[touchStateIndex] = touchedUiElement;
        //     }
        // }

        // Update is called once per frame
        void Update()
        {
            LookAtTarget();
            //ProcessHovered();
        }

        // private void ProcessHovered()
        // {
        //     var statesToDelete = new List<TouchState>();
        //
        //     for (var i = 0; i < touchedUiElementsStates.Count; i++)
        //     {
        //         var touchedUiElementState = touchedUiElementsStates[i];
        //         if (touchedUiElementState.WasHoveredThisFrame)
        //         {
        //             touchedUiElementState.WasHoveredThisFrame = false;
        //             touchedUiElementsStates[i] = touchedUiElementState;
        //         }
        //         else
        //         {
        //             statesToDelete.Add(touchedUiElementState);
        //         }
        //     }
        //
        //     foreach (var touchState in statesToDelete)
        //     {
        //         touchedUiElementsStates.Remove(touchState);
        //         touchState.UiElement.OnDehover();
        //     }
        // }

        private void LookAtTarget()
        {
            transform.forward = transform.position - placementAndLookTarget.position;
        }
    }

    // internal struct TouchState
    // {
    //     public IUiElement UiElement;
    //     public bool WasHoveredThisFrame;
    // }
}