using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeModel : Actor, IPooleable
{
    #region Properties
    [SerializeField] public SlimeStatsSO _stats;
    [SerializeField] private Vector2 positionsToSpawnX;
    [SerializeField] private Vector2 positionsToSpawnZ;
    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;
    public event Action OnDie;

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

        controller.OnRun += Run;
        controller.OnWalk += Walk;
        controller.OnIdle += Idle;
        
    }

    #region Mobile Methods

    public void Move(Vector3 dir)
    {
        var normalizedDir = dir.normalized;
        transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(dir), _stats.RotationSpeed);
        _rb.velocity = new Vector3(normalizedDir.x * CurrentSpeed, _rb.velocity.y, normalizedDir.z * CurrentSpeed);
    }
    public void Idle(Vector3 dir)
    {
        _currentSpeed = 0;
    }
    public void Walk(Vector3 dir)
    {
        _currentSpeed = _stats.WalkSpeed;
        Move(dir);

    }
    public void Run(Vector3 dir)
    {
        _currentSpeed = _stats.RunSpeed;
        Move(dir);

    }
    #endregion
    #region Damagable Methods
    public override void Die()
    {
        Vector3 randomSpawnPos = new Vector3(Random.Range(positionsToSpawnX.x,positionsToSpawnX.y),transform.position.y,Random.Range(positionsToSpawnZ.x,positionsToSpawnZ.y));
        GenericPool.Instance.SpawnFromPool("Slime",randomSpawnPos, Quaternion.identity);
        OnDie.Invoke();
        gameObject.SetActive(false);
    }
    public override void TakeDamage(float dmg)
    {
        _currentLife -= dmg;
        if (_currentLife <= 0)
        {
            Die();
        }
    }
    public void Reset()
    {
        _currentLife = MaxLife;
    }

    public void OnObjectSpawn()
    {
        Reset();
    }
    #endregion

    
}

