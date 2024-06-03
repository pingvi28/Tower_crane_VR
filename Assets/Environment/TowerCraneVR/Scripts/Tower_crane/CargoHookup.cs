using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CargoHookup : MonoBehaviour
{
    public Cargo Cargo { get; private set; }
    public Transform hookPos;

    void Start()
    {
        Cargo = GetComponentInParent<Cargo>();
    }
}