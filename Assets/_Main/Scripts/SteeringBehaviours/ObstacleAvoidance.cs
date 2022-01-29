using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance:ISteering
{
    private Transform _self;
    private Transform _target;
    private float _checkRadius;
    private Collider[] collisions;
    private LayerMask _obstacleLayer;
    private float _multiplier;
    private Dictionary<string, ISteering> behaviours;
    private ISteering _currentBehaviour;
    public bool StopMovement;
    public ObstacleAvoidance(Transform self,Transform target, float radius, int maxObjs,LayerMask obstacleLayers,float multiplier, float targetVel,float timePrediction, float rotSpeed, float ownerSpeed,string desiredBehaviour)
    {
        _self = self;
        _target = target;
        _checkRadius = radius;
        _multiplier = multiplier;
        collisions = new Collider[maxObjs];
        _obstacleLayer = obstacleLayers;

        behaviours = new Dictionary<string, ISteering>();
        SetBehaviours(self, target, targetVel, timePrediction,ownerSpeed,rotSpeed);
        SetNewBehaviour(desiredBehaviour);
        
    }

    private void SetBehaviours(Transform self, Transform target, float targetVel, float timePrediction, float speed, float rotSpeed)
    {
        behaviours.Add("Flee", new FleeBehaviour(self,target,targetVel,timePrediction));
        behaviours.Add("Chase", new ChaseBehaviour(self, target, targetVel, timePrediction));
        behaviours.Add("Wander", new Wander(self, speed,rotSpeed));
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
    public void SetNewBehaviour(string newBehaviourKey)
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

