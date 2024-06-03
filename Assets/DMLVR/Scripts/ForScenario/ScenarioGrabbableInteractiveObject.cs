using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGrabbableInteractiveObject : GrabbableInteractiveObject
{
    public delegate void GrabbableObjectActivatedDelegate();

    public event GrabbableObjectActivatedDelegate GrabbableObjectActivated;

    public override void Activate(InteractiveController interactiveController)
    {
        base.SelectExited(interactiveController);
        GrabbableObjectActivated?.Invoke();
    }
}
