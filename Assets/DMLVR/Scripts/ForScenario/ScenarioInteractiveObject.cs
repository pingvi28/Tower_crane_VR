using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInteractiveObject : InteractiveObject
{
    public delegate void ObjectActivatedDelegate();

    public event ObjectActivatedDelegate ObjectActivated;
    
    public override void SelectEntered(InteractiveController interactiveController)
    {
        base.SelectExited(interactiveController);
        ObjectActivated?.Invoke();
    }
}
