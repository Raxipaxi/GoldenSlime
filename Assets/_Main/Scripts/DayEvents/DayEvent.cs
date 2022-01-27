using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Es de dia");
    }
}
