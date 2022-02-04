using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LineOfSight",menuName = "Steering/AI/LineOfSight", order = 1)]
class LineOfSightStats:ScriptableObject
{
    [SerializeField]private float _angle;
    public float Angle => _angle;

    [SerializeField] private float _range;
    public float Range => _range;

    [SerializeField] private LayerMask _obstacleMask;
    public LayerMask ObstaclesMask => _obstacleMask;

    [SerializeField] private LayerMask _targetMask;
    public LayerMask TargetMask => _targetMask;
}
