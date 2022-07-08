using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float timeToDown;
    [SerializeField] private LayerMask playerLayer;


    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            OpenSesame();
        }
    }
    void OpenSesame()
    {
        Destroy(gameObject);
    }
}
