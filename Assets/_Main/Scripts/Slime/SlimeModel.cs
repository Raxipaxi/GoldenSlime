using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeModel:Actor
{
    #region Properties
    [SerializeField] public SlimeStatsSO _stats;
    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;
    private event Action OnDie;
    
    private float _currentLife;
    public float CurrentLife => _currentLife;
    private Transform _transform;
    private Rigidbody _rb;
    [SerializeField] private LineOfSight _lineOfSight;
    public LineOfSight LineOfSight => _lineOfSight;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        _currentLife = MaxLife;
        _lineOfSight = GetComponent<LineOfSight>();
    }

    public void Suscribe(SlimeController controller)
    {
        controller.OnMove += Move;
        controller.OnPatrol += PatrolHandler;
    }

    #region Mobile Methods

    public void Move(Vector3 dir)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, dir, _currentSpeed * Time.deltaTime);
    }

    private void PatrolHandler()
    {
        _currentSpeed = _stats.WalkSpeed;
    }
    #region Damagable Methods
    public override void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
       // Habria que hacer un pool que se comunique con esto
    }
    public override void TakeDamage(float dmg)
    {
       _currentLife -= dmg;
        if (_currentLife <= 0)
        {
            Die();
        }
    }

    #endregion
#endregion
}

