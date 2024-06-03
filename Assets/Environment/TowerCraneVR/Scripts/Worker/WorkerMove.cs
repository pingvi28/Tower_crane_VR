using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMove : MonoBehaviour
{
    [SerializeField] private Vector3[] destination;
    private NavMeshAgent agent;
    private Vector3 target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = destination[Random.Range(0, destination.Length)];
    }
    
    void Update()
    {
        agent.destination = target;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            target = destination[Random.Range(0, destination.Length)];
        }
    }
}
