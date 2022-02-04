using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "ObstacleAvoidance",menuName = "Steering/AI/ObstacleAvoidance", order = 0)] 
public class ObstacleAvoidanceSO : ScriptableObject
{
    [SerializeField] private Transform _self;
    [SerializeField] private Transform _target;
    [SerializeField] private float _checkRadius;
    [SerializeField] private int _maxObjs;
    [SerializeField] private float _targetRotSpeed;
    [SerializeField] private float _multiplier;
    [SerializeField] private float _timePrediction;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private ObstacleAvoidance.Steering _defaultBehaviour;
    public Transform Self => _self;
    public Transform Target => _target;
    public float CheckRadius => _checkRadius;
    public int MaxObj => _maxObjs;
    public float Multiplier => _multiplier;
    public float TargetRotSpeed => _targetRotSpeed;
    public float TimePrediction => _timePrediction;
    public LayerMask ObstacleLayer => _obstacleLayer;
    public ObstacleAvoidance.Steering DefaultBehaviour => _defaultBehaviour;
}

