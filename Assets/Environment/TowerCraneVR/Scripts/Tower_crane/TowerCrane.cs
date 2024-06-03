using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerCrane : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Cart cart;

    [SerializeField] private Hook hook;
    
    public void CartMove(float scale)
    {
        cart.CartMove(scale);
    }
    
    public void HookMove(float scale)
    {
        hook.HookMove(scale);
    }

    public void TurningTowerCrane(float joystickScale)
    {
        //transform.Rotate(0f, moveSpeed * scale, 0f);
        transform.DORotate(new Vector3(0f, moveSpeed * joystickScale, 0f), 1f, RotateMode.WorldAxisAdd);
    }
}