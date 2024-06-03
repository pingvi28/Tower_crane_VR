using Unity.VisualScripting;
using UnityEngine;

public class Levers : MonoBehaviour, ILevers
{
    [SerializeField] private bool isActive;
    
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    
    [SerializeField] private Vector3 startRotation;
    
    [SerializeField] private Transform target;


    private void Awake()
    {
        startRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive == true)
        {
            var newRotation = Quaternion.LookRotation(target.position - transform.position);
            if (newRotation.eulerAngles.x >  startRotation.x + minAngle &&
                newRotation.eulerAngles.x <  startRotation.x + maxAngle)
            {
                transform.rotation = newRotation;
            }
            
        }
    }

    public void GettingStart()
    {
        isActive = true;
        
    }

    public void GettingEnd()
    {
        isActive = false;
    }

    public void FindTarget()
    {
        
    }
    
    public void LoseTarget()
    {
        
    }
    
}