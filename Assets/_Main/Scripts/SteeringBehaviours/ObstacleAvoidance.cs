using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance: ISteering
{
    private Transform _self;
    private Transform _target;
    private float _checkRadius;
    private Collider[] collisions;
    private LayerMask _obstacleLayer;
    private float _multiplier;
    private Dictionary<Steering, ISteering> behaviours = new Dictionary<Steering, ISteering>();
    private ISteering _currentBehaviour;
    public bool StopMovement;
    public enum Steering
    {
        Flee,
        Chase,
        Wander
    }

    public ObstacleAvoidance(Transform self, Transform target, float radius, int maxObjs, LayerMask obstacleLayers, float multiplier, float targetVel, float timePrediction,Steering desiredBehaviour)
    {
        _self = self;
        _checkRadius = radius;
        _multiplier = multiplier;
        collisions = new Collider[maxObjs];
        behaviours = new Dictionary<Steering, ISteering>();
        _obstacleLayer = obstacleLayers;
        SetBehaviours(self, target, targetVel, timePrediction);
        SetNewBehaviour(desiredBehaviour);
    }
    public ObstacleAvoidance()
    {

    }
    private void SetBehaviours(Transform self, Transform target, float targetVel, float timePrediction)
    {
        var flee = new FleeBehaviour(self, target, targetVel, timePrediction);
        behaviours.Add(Steering.Flee,flee);
        var chase = new ChaseBehaviour(self, target, targetVel, timePrediction);
        behaviours.Add(Steering.Chase, chase);
        var wander = new Wander(self,60f,10f);
        behaviours.Add(Steering.Wander, wander);
    }

    public Vector3 GetDir()
    {
        if (StopMovement == true) return Vector3.zero;

        Vector3 dir = _currentBehaviour.GetDir();
        int countedObjs = Physics.OverlapSphereNonAlloc(_self.transform.position, _checkRadius, collisions,_obstacleLayer);

        Collider nearestObject = null;
        float distanceNearObj = 0;

        for (int i = 0; i < countedObjs; i++)
        {
            var curr = collisions[i];
            if (_self.position == curr.transform.position) continue;
            Vector3 closestPointToSelf = curr.ClosestPointOnBounds(_self.position);
            float distanceCurr = Vector3.Distance(_self.position, closestPointToSelf);
         
            if (nearestObject == null)
            {
                nearestObject = curr;
                distanceNearObj = distanceCurr;
            }
        }
        
        if(nearestObject != null)
        {
            var posObj = nearestObject.transform.position;
            Vector3 dirObstacleToSelf = (_self.position - posObj);
            dirObstacleToSelf = dirObstacleToSelf.normalized * ((_checkRadius - distanceNearObj) / _checkRadius) * _multiplier;
            dir += dirObstacleToSelf;
            dir = dir.normalized;
        }
        return dir;

    }
    public void SetNewBehaviour(Steering newBehaviourKey)
    {
        _currentBehaviour = behaviours[newBehaviourKey];
    }
    public Transform SetTarget 
    {
        set
        {
            _target = value;
        }
    }
}

