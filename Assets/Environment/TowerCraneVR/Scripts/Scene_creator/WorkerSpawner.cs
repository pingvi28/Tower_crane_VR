using UnityEngine;

public class WorkerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject worker;
    [SerializeField] private float spawnRadius = 15f;
    private int count;
    
    void Start()
    {
        SpawnNPCs();

    }

    void SpawnNPCs()
    {
        count = Random.Range(20, 50);
        Vector3 spawnPosition = GetRandomPosition();
        for (int i = 0; i < count; i++)
        {
            Instantiate(worker, spawnPosition, Quaternion.Euler(0, Random.Range(0, 359),0));
        }
    }

    private Vector3 GetRandomPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float radius = Random.Range(0f, spawnRadius);
        
        // Предполагаем, что поверхность на уровне y = 0
        return new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius); 
    }
}
