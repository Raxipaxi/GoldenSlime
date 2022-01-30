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
    private void Start()
    {
        dayEvent.OnDay += SpawnSlime;
    }
    private Vector3 GenerateSlimesPos()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition.y = 1f;
        return spawnPosition;
    }
    private void SpawnSlime()
    {
        
        if(maxSpawnedSlimes <= slimeQuantity) { return; }
        if(maxSpawnedSlimes > slimeQuantity)
        {
            for (int i = 0; i < quantityPerRow; i++)
            {
                slimeQuantity++;
                GenericPool.Instance.SpawnFromPool("Slime", GenerateSlimesPos(), Quaternion.identity);
                GenerateSlimesPos();
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
