using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        [SerializeField] public string tag;
        [SerializeField] public int poolSize;
        [SerializeField] public GameObject objectToPool;
        [SerializeField] public GameObject objectFather;
    }
    public List<Pool> poolsList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static GenericPool Instance;
    private void Awake()
    {
        Instance = this;   
    }
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in poolsList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i <pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.objectToPool);
                if(pool.objectFather != null)
                { 
                    obj.transform.parent = pool.objectFather.transform;
                }
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag,Vector3 posToSpawn, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // Chequeo de si la key existe
        {
            Debug.LogWarning("no existe pool con" + tag);
            return null;
        }
       
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        if (!objectToSpawn.activeSelf)
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = posToSpawn;
            objectToSpawn.transform.rotation = rotation;
        }
        else
        {
            var newObject = Instantiate(objectToSpawn.gameObject);
            newObject.transform.parent = objectToSpawn.transform.parent;
            poolDictionary[tag].Enqueue(objectToSpawn);
            objectToSpawn = newObject;
        }

        
        
        IPooleable pooledObj = objectToSpawn.GetComponent<IPooleable>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
        
    }
}
