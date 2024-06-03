using UnityEngine;

public class GrabbableInteractiveObject : InteractiveObject
{
    private bool isGrab;

    private Vector3 startPosition;
    private Vector3 startRotation;

    private Rigidbody rigi;

    private void Awake()
    {
        if (rigi == null)
        {
            rigi = GetComponent<Rigidbody>();
        }

        var transform1 = transform;
        startPosition = transform1.position;
        startRotation = transform1.eulerAngles;
    }

    private void ReturnStartTransform()
    {
        var transform1 = transform;
        transform1.position = startPosition;
        transform1.eulerAngles = startRotation;
    }

    public bool IsGrab
    {
        get { return isGrab; }
    }

    private void Grab(InteractiveController interactiveController)
    {
        if (isGrab == false)
        {
            isGrab = true;
            Transform transform1;
            (transform1 = transform).SetParent(interactiveController.AttachTransform);
            transform1.localPosition = Vector3.zero;
            rigi.isKinematic = true;
            rigi.useGravity = false;
        }
    }

    private void LetGo()
    {
        if (isGrab == true)
        {
            isGrab = false;
            transform.SetParent(null);
            ReturnStartTransform();
        }
    }

    public void Placement(Transform placeTransform)
    {
        isGrab = false;
        Transform transform1;
        (transform1 = transform).SetParent(null);
        transform1.position = placeTransform.position;
        transform1.rotation = placeTransform.rotation;
    }

    public override void Activate(InteractiveController interactiveController)
    {
        if (isGrab == false)
        {
            base.Activate(interactiveController);
            Grab(interactiveController);
        }
        else
        {
            if (interactiveController.AttachTransform == transform.parent)
            {
                LetGo();
            }
        }
    }
}