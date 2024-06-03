using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Cargo : MonoBehaviour
{
    public float massInKg;
    private Rigidbody _rigidbody;
    private FixedJoint _fixedJoint;
    public Material cargoMat;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public virtual void UpdateState(bool isHooked, Rigidbody hookRigidbody = null)
    {
        if (isHooked)
        {
            _fixedJoint = transform.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = hookRigidbody;
        }
        else
        {
            Destroy(_fixedJoint);
        }
    }

    public virtual void CheckZone()
    {
    }
}