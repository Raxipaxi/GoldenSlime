using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletDmg : MonoBehaviour, IPooleable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;
    private float _currentLifeTime;
    [SerializeField] private LayerMask playerMask;
    private bool wasShooted;
    private void OnCollisionEnter(Collision collision)
    {
        if ((playerMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            var colls = collision.gameObject.GetComponent<iDamageable>();
            colls?.TakeDamage(_damage);
        }
        
        
    }
    private void Update()
    {
        if(wasShooted == true)
        {
            _currentLifeTime -= Time.deltaTime;
            if(_currentLifeTime <= 0)
            {
                OnDisappear();
            }
        }
    }
    public void SetDamage(float dmg)
    {
        _damage = dmg;
    }
    public void OnDisappear()
    {
        gameObject.SetActive(false);
        wasShooted = false;
    }
    public void OnObjectSpawn()
    {
        _currentLifeTime = _lifeTime;
        wasShooted = true;
    }
}
