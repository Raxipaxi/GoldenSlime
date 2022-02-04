using System;
using UnityEngine;

public class DayEvent : MonoBehaviour
{
    public event Action OnDay; 
    private void OnTriggerEnter(Collider other)
    {
        
       OnDay?.Invoke();
    }
}
