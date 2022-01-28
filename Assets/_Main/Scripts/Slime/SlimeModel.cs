using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeModel:Actor
{
    #region
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float life;
   
    private float CurrentLife => life;
    private Transform _transform;
    private Rigidbody _rb;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;    
    }

    public void Suscribe(SlimeController controller)
    {
        controller.OnIdle += Idle;
        controller.OnMove += Walk;
    }

    #region Mobile Methods
    public override void Walk(Vector3 dir)
    {
        Move(dir, walkSpeed);
        currentSpeed = walkSpeed;
    
    }

    public override void LookDir(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            _transform.localRotation *= Quaternion.Euler(dir.x * Time.deltaTime * 360, 0, 0);
        }
        transform.forward = dir;
    }

    public override void Idle()
    {
        _rb.velocity = Vector3.zero;
    }

    public override void Run(Vector3 dir)
    {
        Move(dir, runSpeed);
        currentSpeed = runSpeed;
    }

    #region Damagable Methods
    public override void Die()
    {
        //OnDie?.Invoke();
        //Habria que hacer un pool que se comunique con esto
    }
    public override void TakeDamage(float dmg)
    {
        life -= dmg;
        if (CurrentLife <= 0)
        {
            Die();
        }
    }

    #endregion
#endregion
}

