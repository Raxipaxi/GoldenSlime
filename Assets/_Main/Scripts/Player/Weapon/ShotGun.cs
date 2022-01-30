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
        for (int i = 0; i < pellets.Count; i++)
        {
            pellets[index] = Random.rotation;
            GameObject pelletInst = Instantiate(this.pellet, barrelExit.position, barrelExit.rotation);
            pelletInst.transform.rotation = Quaternion.RotateTowards(pelletInst.transform.rotation, pellets[index],spreadAngle);
            pelletInst.GetComponent<PelletDmg>().SetDamage(damage);
            pelletInst.GetComponent<Rigidbody>().AddForce(pelletInst.transform.forward * pelletVel);
            index++;
        }
   
    }
}
