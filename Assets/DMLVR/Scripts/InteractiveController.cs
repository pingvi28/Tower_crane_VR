using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractiveController : MonoBehaviour
{
    [SerializeField] private Transform attachTransform;

    [SerializeField] private InteractionSystem interactionSystem;

    [SerializeField] public XRDirectInteractor xRDirectInteractor;

    public Transform AttachTransform
    {
        get { return attachTransform; }
    }

    private void Awake()
    {
        if (interactionSystem == null)
        {
            interactionSystem = GetComponent<InteractionSystem>();
        }
    }

    public void SendingTurnRequest(InputAction.CallbackContext obj)
    {
        interactionSystem.TurnInteraction(obj.ReadValue<Vector2>());
    }

    public void SendingSelectEnteredRequest(BaseInteractive baseInteractive)
    {
        interactionSystem.SelectEnteredInteraction(baseInteractive, this);
    }

    public void SendingSelectExitedRequest(BaseInteractive baseInteractive)
    {
        interactionSystem.SelectExitedInteraction(baseInteractive, this);
    }

    public void SendingActionRequest(BaseInteractive baseInteractive)
    {
        interactionSystem.ActionInteraction(baseInteractive, this);
    }

    public void SendingTeleportationRequest(InteractiveTeleportAnchor interactiveTeleportAnchor)
    {
        interactionSystem.TeleportationInteraction(interactiveTeleportAnchor, this);
    }

    public void HoverRequest(BaseInteractive baseInteractive)
    {
        interactionSystem.HoverInteraction(baseInteractive, this);
    }

    public void DehoverRequest(BaseInteractive baseInteractive)
    {
        interactionSystem.DehoverInteraction(baseInteractive, this);
    }
}