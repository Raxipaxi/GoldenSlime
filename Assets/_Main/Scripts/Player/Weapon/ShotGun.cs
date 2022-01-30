﻿using System.Collections.Generic;
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

    void Fire()
    {
        int index = 0 ;
        foreach (var quat in pellets)
        {
            pellets[index] = Random.rotation;
            GameObject pelletInst = Instantiate(this.pellet, barrelExit.position, barrelExit.rotation);
            pelletInst.transform.rotation = Quaternion.RotateTowards(pelletInst.transform.rotation, pellets[index],spreadAngle);
            pelletInst.GetComponent<Rigidbody>().AddForce(pelletInst.transform.right * pelletVel);
            index++;
        }
    }
}
