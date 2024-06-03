using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInteractive : MonoBehaviour, IInteractive
{
    private bool isHover;

    private int contactsNumber;

    [SerializeField] protected UnityEvent hover;
    [SerializeField] protected UnityEvent dehover;
    [SerializeField] protected UnityEvent selectEntered;
    [SerializeField] protected UnityEvent selectExited;
    [SerializeField] protected UnityEvent activate;

    public bool IsHover
    {
        get { return isHover; }
    }

    public virtual void Hover(InteractiveController interactiveController)
    {
        contactsNumber++;
        if (contactsNumber == 1)
        {
            isHover = true;
            hover.Invoke();
        }
    }

    public virtual void Dehover(InteractiveController interactiveController)
    {
        contactsNumber--;
        if (contactsNumber == 0)
        {
            isHover = false;
            dehover.Invoke();
        }
    }

    public virtual void SelectEntered(InteractiveController interactiveController)
    {
        selectEntered.Invoke();
    }

    public virtual void SelectExited(InteractiveController interactiveController)
    {
        selectExited.Invoke();
    }

    public virtual void Activate(InteractiveController interactiveController)
    {
        activate.Invoke();
    }
}