using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShotGun : MonoBehaviour
{
    public int pelletCount;
    public float spreadAngle;
    public float pelletVel;
    public GameObject pellet;
    public Transform barrelExit;
    private List<Quaternion> pellets;
    public Vector3 offset;
    private void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    public void Fire(float damage)
    {
        int index = 0 ;
        for (int i = 0; i < pellets.Count - 1; i++)
        {
            var randomPos = Random.insideUnitSphere;
            pellets[index] = Random.rotation;
            GameObject pelletInst = GenericPool.Instance.SpawnFromPool("Bullet", barrelExit.position, barrelExit.rotation);
            //GameObject pelletInst = Instantiate(this.pellet, barrelExit.position, barrelExit.rotation);
            pelletInst.transform.rotation = Quaternion.RotateTowards(pelletInst.transform.rotation, pellets[index], spreadAngle);
            pelletInst.GetComponent<Rigidbody>().AddForce(transform.up * pelletVel, ForceMode.Acceleration);
            index++;
        }
       
    }
    public void Reload()
    {


    }
}
