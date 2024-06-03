using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Cart : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minLimit;
    [SerializeField] private float maxLimit;

    public void CartMove(float scale)
    {
        var newZ = Mathf.Clamp(transform.localPosition.z + moveSpeed * scale * Time.deltaTime, minLimit, maxLimit);
        // transform.position = new Vector3(transform.position.x, transform.position.y,
        //     Mathf.Clamp(transform.position.z, minLimit, maxLimit));

        transform.DOLocalMoveZ(newZ, 1f);
    }

    private void Update()
    {
        Debug.DrawLine(transform.position,new Vector3(transform.position.x,0f,transform.position.z),Color.red);
    }
}