using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightInteractiveLever : InteractiveObject
{
    [SerializeField] private bool isActive;

    [SerializeField] private float deflectionAngleLimitation;
    [SerializeField] private float requiredDotProduct;
    [SerializeField] private float leverDeadzoneAngle = 10f;
    [SerializeField] [Range(0, 1)] private float hapticPower;
    [SerializeField] [Range(0, 1)] private float hapticDuration;

    [SerializeField] private Quaternion startLocalRotation;

    [SerializeField] private Transform target;
    [SerializeField] private Transform leverBase;
    [SerializeField] private Transform rotationalTarget;

    [SerializeField] private TowerCrane towerCrane;

    [SerializeField] private bool flipHorizontalResponse;
    [SerializeField] private bool flipVerticalResponse;

    [SerializeField] private XRDirectInteractor xRDirectInteractor;


    private void Awake()
    {
        startLocalRotation = rotationalTarget.localRotation;
        requiredDotProduct = Vector3.Dot(Vector3.forward,
            Quaternion.AngleAxis(deflectionAngleLimitation, Vector3.right) * Vector3.forward);
    }

    private void Update()
    {
        if (isActive)
        {
            Vector3 newDirection = (target.position - rotationalTarget.position).normalized;
            Quaternion newRotation = Quaternion.LookRotation(newDirection, leverBase.right);
            //Calc dot product between up vector of our level holder and new rotation for lever
            //Debug.Log("Dot: " + Vector3.Dot(leverBase.rotation * Vector3.up,
            //newRotation * Vector3.forward));
            // compare vector angle is okay for our rotation. the higher Dot value, the more parallel vectors are
            if (Vector3.Dot(leverBase.rotation * Vector3.up,
                    newRotation * Vector3.forward) > requiredDotProduct)
            {
                rotationalTarget.rotation = newRotation;
            }

            // Debug.Log("angle: " + Vector3.SignedAngle((newRotation * Vector3.right).normalized,
            //     leverBase.forward.normalized, leverBase.forward));
            //
            // if (Vector3.SignedAngle((newRotation * Vector3.up).normalized,
            //         leverBase.right.normalized, leverBase.forward) == 0)
            // {
            //     rotationalTarget.rotation = newRotation;
            // }
            var currentLeverAngleVertical = rotationalTarget.right.normalized;
            var verticalLeverAxis = leverBase.forward.normalized;
            float b = Vector3.SignedAngle(currentLeverAngleVertical, verticalLeverAxis,
                flipVerticalResponse ? leverBase.right : -leverBase.right);
            Quaternion fixRotation = Quaternion.Euler(-90f + b, 0, -90f);
            rotationalTarget.localRotation = fixRotation;
            
            //var currentLeverAngleHorizontal = rotationalTarget.up.normalized;
            //var horizontalLeverAxis = leverBase.right.normalized;
            //var currentLeverAngleVertical = rotationalTarget.right.normalized;
            //var verticalLeverAxis = leverBase.forward.normalized;

            //  var a = Vector3.SignedAngle(currentLeverAngleHorizontal, horizontalLeverAxis,
            //flipHorizontalResponse ? -leverBase.forward : leverBase.forward);
            // Debug.Log("a: " + a);
            //var b = Vector3.SignedAngle(currentLeverAngleVertical, verticalLeverAxis,
            //flipVerticalResponse ? leverBase.right : -leverBase.right);
            //Debug.Log("b: " + b);

            //Debug.DrawLine(rotationalTarget.position, rotationalTarget.position + newDirection1, Color.blue);
            //Debug.DrawLine(rotationalTarget.position, rotationalTarget.position + newDirection2, Color.green);
            //Debug.DrawLine(rotationalTarget.position, rotationalTarget.position + newDirection3, Color.yellow);
            //Debug.DrawLine(rotationalTarget.position, rotationalTarget.position + newDirection4, Color.red);

            //check for minimal
            // if (Math.Abs(a) > leverDeadzoneAngle)
            // {
            //     towerCrane.TurningTowerCrane(a / deflectionAngleLimitation);
            // }
            //
            // if (Math.Abs(b) > leverDeadzoneAngle)
            // {
            //     towerCrane.CartMove(b / deflectionAngleLimitation);
            // }

            if (Math.Abs(b) > leverDeadzoneAngle)
            {
                towerCrane.HookMove(b / deflectionAngleLimitation);
                //xRDirectInteractor.SendHapticImpulse(Math.Abs(b / deflectionAngleLimitation * hapticPower), hapticDuration);
            }
        }
    }

    public override void Hover(InteractiveController interactiveController)
    {
        base.Hover(interactiveController);
        if (target == null)
        {
            target = interactiveController.transform;
            xRDirectInteractor = interactiveController.xRDirectInteractor;
        }
    }

    public override void Dehover(InteractiveController interactiveController)
    {
        base.Dehover(interactiveController);
        target = null;
        GettingEnd();
        xRDirectInteractor = null;
    }

    public override void SelectEntered(InteractiveController interactiveController)
    {
        base.SelectEntered(interactiveController);
        if (isActive == false)
        {
            GettingStart();
        }
    }

    public override void SelectExited(InteractiveController interactiveController)
    {
        base.SelectExited(interactiveController);

        GettingEnd();
    }

    private void GettingStart()
    {
        isActive = true;
    }

    private void GettingEnd()
    {
        isActive = false;
        rotationalTarget.localRotation = startLocalRotation;
    }
}