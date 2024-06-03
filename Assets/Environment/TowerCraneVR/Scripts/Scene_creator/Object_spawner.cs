using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Object_spawner : MonoBehaviour
{
    [SerializeField]private GameObject[] spawn_object;
    [SerializeField]private GameObject point_to_spawn;
    
    void Start()
    {
        Transform[] point_transform = point_to_spawn.GetComponentsInChildren<Transform>();

        for (int i = 0; i < point_transform.Length - 1; i++)
        {
            Instantiate(spawn_object[Random.Range(0, spawn_object.Length)], point_transform[i+1].position, 
                Quaternion.Euler(0, Random.Range(0, 359),0));
            Destroy(point_transform[i].gameObject);
        }
    }
}
