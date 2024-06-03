using System.Collections;
using System.Collections.Generic;
using ScenarioSystem.Scripts.Interfaces;
using ScenarioSystem.Scripts.Steps;
using UnityEngine;

public class GrabbingCargo : ScenarioStep
{
    [SerializeField] private string targetGameObjectName;

    private IStepLauncher _launcher;

    private bool _isLaunched;

    private ScenarioCargo scenarioCargo;

    public override bool IsLaunched()
    {
        return _isLaunched;
    }

    public override void Launch(IStepLauncher launcher)
    {
        _launcher = launcher;
        scenarioCargo = _launcher.GetResources().GetGameObject(targetGameObjectName)
            .GetComponent<ScenarioCargo>();

        scenarioCargo.CargoInteraction += InteractObject;

        _isLaunched = true;
    }

    private void InteractObject()
    {
        _launcher.StepFinished(this);
        scenarioCargo.CargoInteraction -= InteractObject;
    }

    public override void Stop(IStepLauncher launcher)
    {

    }
}
