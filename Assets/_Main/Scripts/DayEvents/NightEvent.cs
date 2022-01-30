using System;
using UnityEngine;

public class  NightEvent : MonoBehaviour
{
    public event Action OnNight;
    private void OnTriggerEnter(Collider other)
    {
        OnNight?.Invoke();
        
    }

}
