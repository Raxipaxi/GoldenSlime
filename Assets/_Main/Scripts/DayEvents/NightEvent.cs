using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other!=null)
         Debug.Log("Se hizo de noche vite");
        
    }
    // private void OnTrig
    // {
    //     Debug.Log("Se hizo de noche vite");
    // }
}
