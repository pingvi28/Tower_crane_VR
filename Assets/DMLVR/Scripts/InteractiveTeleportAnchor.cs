using UnityEngine;

public class InteractiveTeleportAnchor : BaseInteractive
{
    [SerializeField] protected Transform anchor;

    public Transform Anchor
    {
        get { return anchor; }
    }

    public void Enabling()
    {
        gameObject.SetActive(true);
    }

    public void Disabling()
    {
        gameObject.SetActive(false);
    }
}