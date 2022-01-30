using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlimeBaseStats", menuName = "SlimeStats", order = 2)]
    public class SlimeStatsSO : ScriptableObject
    {
    [SerializeField] private float _walkSpeed;
    public float WalkSpeed => _walkSpeed;
    [SerializeField] private float _runSpeed;
    public float RunSpeed => _runSpeed;
    [SerializeField] private float _life;
    public float MaxLife => _life;
    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;
    [SerializeField]private float attackCooldown;
    public float AttackCooldown => attackCooldown; 
}
