using System.Collections.Generic;
using UnityEngine;

public class CheckInteractiveObject : MonoBehaviour
{
    [SerializeField] private InteractiveController interactiveController;

    [SerializeField] private List<InteractiveObject> interactiveObjects;

    private void Awake()
    {
        if (interactiveController == null)
        {
            interactiveController = GetComponent<InteractiveController>();
        }
    }

    private void Connection(InteractiveObject interactive)
    {
        interactiveObjects.Add(interactive);
        interactiveController.HoverRequest(interactiveObjects[0]);
    }

    private void Disconnection(int i)
    {
        interactiveController.DehoverRequest(interactiveObjects[i]);
        interactiveObjects.RemoveAt(i);
        CheckNextObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<InteractiveObject>())
        {
            Connection(other.transform.GetComponent<InteractiveObject>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<InteractiveObject>())
        {
            for (int i = 0; i < interactiveObjects.Count; i++)
            {
                if (interactiveObjects[i] == other.transform.GetComponent<InteractiveObject>())
                {
                    Disconnection(i);
                }
            }
        }
    }

    private void CheckNextObject()
    {
        if (interactiveObjects.Count > 0)
        {
            Connection(interactiveObjects[0]);
        }
    }

    public void SendSelectEntered()
    {
        if (interactiveObjects.Count > 0)
        {
            interactiveController.SendingSelectEnteredRequest(interactiveObjects[0]);
        }
    }

    public void SendSelectExited()
    {
        if (interactiveObjects.Count > 0)
        {
            interactiveController.SendingSelectExitedRequest(interactiveObjects[0]);
        }
    }

    public void SendAction()
    {
        if (interactiveObjects.Count > 0)
        {
            interactiveController.SendingActionRequest(interactiveObjects[0]);
        }
    }
}