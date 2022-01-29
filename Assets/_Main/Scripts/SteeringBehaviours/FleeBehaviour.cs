using System;
using System.Collections.Generic;
using UnityEngine;
class FleeBehaviour : ISteering
{
    private Transform _self;
    private Transform _target;
    public float _velocity { get; private set; }
    private float _timePrediction;

    public FleeBehaviour(Transform self, Transform target, float velocity, float timePrediction)
    {
        _self = self;
        _target = target;
        _velocity = velocity;
        _timePrediction = timePrediction;
    }

    public Transform SetTarget
    {
        set
        {
            _target = value;
        }
    }
    public Transform SetSelf
    {
        set
        {
            _self = value;
        }
    }
    public Vector3 GetDir()
    {
        float directionMultipliter = (_velocity * _timePrediction);
        float distance = Vector3.Distance(_target.position, _self.position);

        if (directionMultipliter >= distance)
        {
            directionMultipliter = distance / 2;
        }
        Vector3 finalPos = _target.position + _target.forward * directionMultipliter;
        Vector3 dir = (finalPos - _self.position).normalized;
        return dir;
    }
}

