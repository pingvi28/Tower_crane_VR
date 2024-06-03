using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private TeleportationSystem teleportationSystem;

    [SerializeField] private TurnSystem turnSystem;

    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        if (teleportationSystem == null)
        {
            teleportationSystem = GetComponent<TeleportationSystem>();
        }

        if (turnSystem == null)
        {
            turnSystem = GetComponent<TurnSystem>();
        }

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    public void TurnInteraction(Vector2 input)
    {
        if (turnSystem != null)
        {
            TeleportationInteraction(turnSystem.VRTurn(playerCamera.transform.position, input));
        }
        else
        {
            return;
        }
    }

    public void TeleportationInteraction(Vector3 teleportPoint)
    {
        if (teleportationSystem != null)
        {
            teleportationSystem.Teleportation(teleportPoint, playerCamera.transform.position);
        }
        else
        {
            return;
        }
    }

    public void TeleportationInteraction(InteractiveTeleportAnchor interactiveTeleportAnchor,
        InteractiveController interactiveController)
    {
        teleportationSystem.Teleportation(interactiveTeleportAnchor, playerCamera.transform.position);
        interactiveTeleportAnchor.SelectExited(interactiveController);
    }

    public void HoverInteraction(BaseInteractive baseInteractive, InteractiveController interactiveController)
    {
        baseInteractive.Hover(interactiveController);
    }

    public void DehoverInteraction(BaseInteractive baseInteractive, InteractiveController interactiveController)
    {
        baseInteractive.Dehover(interactiveController);
    }

    public void SelectEnteredInteraction(BaseInteractive baseInteractive, InteractiveController interactiveController)
    {
        baseInteractive.SelectEntered(interactiveController);
    }

    public void SelectExitedInteraction(BaseInteractive baseInteractive, InteractiveController interactiveController)
    {
        baseInteractive.SelectExited(interactiveController);
    }

    public void ActionInteraction(BaseInteractive baseInteractive, InteractiveController interactiveController)
    {
        baseInteractive.Activate(interactiveController);
    }
}