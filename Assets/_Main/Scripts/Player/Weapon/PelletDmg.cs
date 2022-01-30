using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletDmg : MonoBehaviour
{
    private float _damage;
    private float _detroy;
    [SerializeField] private LayerMask playerMask;
    private void OnCollisionEnter(Collision collision)
    {
        if ((playerMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            var colls = collision.gameObject.GetComponent<iDamageable>();
            colls?.TakeDamage(_damage);
        }
        else
        {
            
            //TODO acá pool de balas
            Destroy(this);
        }
        
        
    }

    public void SetDamage(float dmg)
    {
        _damage = dmg;
    }
}
