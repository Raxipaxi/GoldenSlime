using System.Collections;
using UnityEngine;
using System;
public class SlimePatrolState<T> : State<T>
{
    private INode _root;
    private Action<Vector3> _onWalk;
    private Func<bool> _isSeen;
    private Transform _transform;
    private float _minDistance;
    private ObstacleAvoidance _obstacleAvoidance;
    public SlimePatrolState(Func<bool> isSeen, Action<Vector3> onWalk, Action onStartPatrol,INode root, Transform transform, ObstacleAvoidance obstacleAvoidance)
    {
        _isSeen = isSeen;
        _root = root;
        _onWalk = onWalk;
        _transform = transform;
        _obstacleAvoidance = obstacleAvoidance;
        onStartPatrol.Invoke();
        
    }
    public override void Awake()
    {
        _obstacleAvoidance.SetNewBehaviour(ObstacleAvoidance.Steering.Wander);
        _obstacleAvoidance.GetDir();
    }
    public override void Execute()
    {
        var canSee = _isSeen.Invoke();
        if (canSee)
        {
            _root.Execute();
            return;
        }
        var dir = _obstacleAvoidance.GetDir();
        var dist = Vector3.Distance(dir, _transform.position);
        if(dist <= 0.5f)
        {
            Debug.Log("A");
            _obstacleAvoidance.GetDir();
        }
        dir.y = 0;
        _onWalk?.Invoke(dir);
    }
}
