using System;
using UnityEngine;

public class CheckInteractiveTeleport : MonoBehaviour
{
    [SerializeField] private bool raycastIsStart;

    [SerializeField] private float raycastDistance;

    [SerializeField] private LineRenderer handLine;

    [SerializeField] private InteractiveController interactiveController;

    [SerializeField] private InteractiveTeleportAnchor interactiveTeleportAnchor;

    private void Awake()
    {
        if (handLine == null)
        {
            handLine = GetComponent<LineRenderer>();
        }

        if (interactiveController == null)
        {
            interactiveController = GetComponent<InteractiveController>();
        }
    }

    private void Update()
    {
        if (raycastIsStart == true)
        {
            HandRaycast();
        }
        else
        {
            handLine.enabled = false;

            if (interactiveTeleportAnchor != null)
            {
                Disconnection();
            }
        }
    }

    public bool Activation()
    {
        return raycastIsStart = true;
    }

    public bool Deactivation()
    {
        return raycastIsStart = false;
    }

    private void HandRaycast()
    {
        var myTransform = transform;

        if (Physics.Raycast(myTransform.position, myTransform.forward, out RaycastHit hit, raycastDistance))
        {
            handLine.enabled = true;
            handLine.SetPosition(0, transform.position);
            handLine.SetPosition(1, hit.point);
            if (hit.transform.GetComponent<InteractiveTeleportAnchor>())
            {
                if (interactiveTeleportAnchor == null)
                {
                    Connection(hit.transform.GetComponent<InteractiveTeleportAnchor>());
                }
            }
            else
            {
                if (interactiveTeleportAnchor != null)
                {
                    Disconnection();
                }
            }
        }
        else
        {
            handLine.enabled = false;
        }
    }

    private void Connection(InteractiveTeleportAnchor interactive)
    {
        interactiveTeleportAnchor = interactive;
        interactiveController.HoverRequest(interactiveTeleportAnchor);
    }

    private void Disconnection()
    {
        interactiveController.DehoverRequest(interactiveTeleportAnchor);
        interactiveTeleportAnchor = null;
    }

    public void SendSelectEntered()
    {
        if (interactiveTeleportAnchor != null)
        {
            interactiveController.SendingSelectEnteredRequest(interactiveTeleportAnchor);
        }
    }

    public void SendSelectExited()
    {
        if (interactiveTeleportAnchor != null)
        {
            interactiveController.SendingSelectExitedRequest(interactiveTeleportAnchor);
            interactiveController.SendingTeleportationRequest(interactiveTeleportAnchor);
        }
    }
}