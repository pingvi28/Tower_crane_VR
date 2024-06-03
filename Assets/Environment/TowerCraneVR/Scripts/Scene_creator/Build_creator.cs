using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Build_creator : MonoBehaviour
{
    [SerializeField]private GameInfo gameInfo;
    [SerializeField]private GameObject[] build_part ;
    [SerializeField]private GameObject[] position ;
    
    private int[] floor;

    void Start()
    {
        floor = gameInfo.getBuildingHeight();

        for (int i = 0; i < floor.Length; i++)
        {
            for (int j = 0; j < floor[i]; j++)
            {
                Instantiate(build_part[i], new Vector3(position[i].transform.position.x/2, 2.1f * j, 
                    position[i].transform.position.z/2), Quaternion.Euler(0, 0, 0));
                
                if (i == (floor.Length - 1) && j == (floor[i] -1))
                {
                    Instantiate(build_part[2], new Vector3(position[i].transform.position.x, 
                        4.27f * (j + 1), position[i].transform.position.z), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }
}
