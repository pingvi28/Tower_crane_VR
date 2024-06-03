using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCargo : Cargo
{
    public delegate void CargoInteractionDelegate();
    public event CargoInteractionDelegate CargoInteraction;
    
    public override void UpdateState(bool isHooked, Rigidbody hookRigidbody = null)
    {
        base.UpdateState(isHooked, hookRigidbody);
        if (isHooked)
        {
            CargoInteraction?.Invoke();
        }
    }
}
