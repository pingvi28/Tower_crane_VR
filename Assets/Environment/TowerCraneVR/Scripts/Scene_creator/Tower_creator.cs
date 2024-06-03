using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tower_creator : MonoBehaviour
{
    public GameObject[] tower_crane_part;
    public Vector3[] positions  = new Vector3[2];
    
    [SerializeField]private GameInfo gameInfo;
    [SerializeField] private GameObject camera;
    [SerializeField] private float increase;
    
    private int[] crane_level;
    private int count_crane ;

    
    // Start is called before the first frame update
    void Start()
    {
        crane_level = gameInfo.getBuildingHeight();
        count_crane = crane_level.Length;
        for (int i = 0; i < count_crane; i++)
        {
            float z = Random.Range(-29.0f, 0f);
            Instantiate(tower_crane_part[0], 
                new Vector3(positions[i][0], positions[i][1], positions[i][2] + z), 
                Quaternion.Euler(0, 0, 0));

            float increase_c_l = crane_level[i] * increase;
            for (int j = 0; j < increase_c_l; j++)
            {
                Instantiate(tower_crane_part[1],
                    new Vector3(positions[i][0], positions[i][1] + 0.61f + 3.55f * j, positions[i][2] + z),
                    Quaternion.Euler(0, 0, 0));
            }

            
            if (i == (count_crane - 1))
            {
                GameObject head = Instantiate(tower_crane_part[3],
                    new Vector3(positions[i][0], positions[i][1] + 0.61f + 3.55f * increase_c_l, positions[i][2]+ z),
                    Quaternion.Euler(0, Random.Range(0, 359), 0));
                
                camera.transform.position = head.transform.position;
            }
            else
            {
                Instantiate(tower_crane_part[2],
                    new Vector3(positions[i][0], positions[i][1] + 0.61f + 3.55f * increase_c_l, positions[i][2]+ z),
                    Quaternion.Euler(0, Random.Range(0, 359), 0));
            }

        }
    }
}
