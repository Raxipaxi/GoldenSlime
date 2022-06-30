using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int maxSpawnedSlimes;
    [SerializeField] private int quantityPerRow;
    [SerializeField] private DayEvent dayEvent;
    private int slimeQuantity;
    [SerializeField] Transform[] spawnPoints;
    private void Start()
    {
        dayEvent.OnDay += SpawnSlime;
    }
    private Vector3 GenerateSlimesPos()
    {
        var spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        return spawnPoint.position;
    }
    private void SpawnSlime()
    {
        
        if(maxSpawnedSlimes <= slimeQuantity) { return; }
        if(maxSpawnedSlimes > slimeQuantity)
        {
            for (int i = 0; i < quantityPerRow; i++)
            {
                slimeQuantity++;
                GenerateSlimesPos();
                GenericPool.Instance.SpawnFromPool("Slime", GenerateSlimesPos(), Quaternion.identity);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
