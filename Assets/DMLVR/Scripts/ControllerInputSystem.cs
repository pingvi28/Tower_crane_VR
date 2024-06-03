using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputSystem : MonoBehaviour
{
    [SerializeField] private CheckInteractiveTeleport checkInteractiveTeleport;

    [SerializeField] private CheckInteractiveObject checkInteractiveObject;

    [SerializeField] private InteractiveController interactiveController;

    [SerializeField] private ActionBasedController actionBasedController;

    private void Awake()
    {
        if (actionBasedController == null)
        {
            actionBasedController = GetComponent<ActionBasedController>();
        }

        if (checkInteractiveTeleport == null)
        {
            checkInteractiveTeleport = GetComponent<CheckInteractiveTeleport>();
        }

        if (checkInteractiveObject == null)
        {
            checkInteractiveObject = GetComponent<CheckInteractiveObject>();
        }

        if (interactiveController == null)
        {
            interactiveController = GetComponent<InteractiveController>();
        }

        actionBasedController.selectAction.action.started += CreateTurnRequest;
        actionBasedController.uiPressAction.action.started += StartRaycastCheck;
        actionBasedController.uiPressAction.action.canceled += EndRaycastCheck;
        actionBasedController.activateAction.action.started += CreateSelectEnteredRequest;
        actionBasedController.activateAction.action.canceled += CreateSelectExitedRequest;
        actionBasedController.activateActionValue.action.started += CreateActionRequest;
    }

    private void OnDestroy()
    {
        actionBasedController.selectAction.action.started -= CreateTurnRequest;
        actionBasedController.uiPressAction.action.started -= StartRaycastCheck;
        actionBasedController.uiPressAction.action.canceled -= EndRaycastCheck;
        actionBasedController.activateAction.action.started -= CreateSelectEnteredRequest;
        actionBasedController.activateAction.action.canceled -= CreateSelectExitedRequest;
        actionBasedController.activateActionValue.action.started -= CreateActionRequest;
    }

    private void CreateTurnRequest(InputAction.CallbackContext obj)
    {
        interactiveController.SendingTurnRequest(obj);
    }

    private void CreateSelectEnteredRequest(InputAction.CallbackContext obj)
    {
        InteractiveTeleportSelectEntered();
        InteractiveObjectSelectEntered();
    }

    private void CreateSelectExitedRequest(InputAction.CallbackContext obj)
    {
        InteractiveTeleportSelectExited();
        InteractiveObjectSelectExited();
    }

    private void StartRaycastCheck(InputAction.CallbackContext obj)
    {
        if (checkInteractiveObject != null)
        {
            checkInteractiveTeleport.Activation();
        }
    }

    private void EndRaycastCheck(InputAction.CallbackContext obj)
    {
        if (checkInteractiveTeleport != null)
        {
            checkInteractiveTeleport.Deactivation();
        }
    }

    private void CreateActionRequest(InputAction.CallbackContext obj)
    {
        checkInteractiveObject.SendAction();
    }

    private void InteractiveTeleportSelectEntered()
    {
        if (checkInteractiveTeleport != null)
        {
            checkInteractiveTeleport.SendSelectEntered();
        }
    }

    private void InteractiveObjectSelectEntered()
    {
        checkInteractiveObject.SendSelectEntered();
    }

    private void InteractiveTeleportSelectExited()
    {
        
        if (checkInteractiveTeleport != null)
        {
            checkInteractiveTeleport.SendSelectExited();
        }
    }

    private void InteractiveObjectSelectExited()
    {
        checkInteractiveObject.SendSelectExited();
    }
}