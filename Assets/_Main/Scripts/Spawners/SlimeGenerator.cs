using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int maxSpawnedSlimes;
    private void Update()
    {
        
    }
    private void SlimeCreator()
    {
        
    }
    private void SpawnSlime(Vector3 posToSpawn)
    {
        GenericPool.Instance.SpawnFromPool("Slime", posToSpawn, Quaternion.identity);
    }
}
