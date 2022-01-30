using System.Collections;
using UnityEngine;
using System;
public class SlimeChaseState<T> : State<T>
{
    private Transform _target;

    private INode _root;
    private ObstacleAvoidance _behaviour;

    private Action<Vector3> _onChase;
    private float _timeToAttemptAttack;
    private float _counter;
    public SlimeChaseState(Transform target, INode root, ObstacleAvoidance behaviour, Action<Vector3> onChase, float timeToAttemptAttack)
    {
        _root = root;
        _behaviour = behaviour;
        _onChase = onChase;
        _timeToAttemptAttack = timeToAttemptAttack;
        _target = target;
    }

    private void ResetCounter()
    {
        _counter = _timeToAttemptAttack;
    }
    public override void Awake()
    {
        _behaviour.SetNewBehaviour(ObstacleAvoidance.Steering.Chase);
        _behaviour.SetTarget = _target;

        ResetCounter();
    }

    public override void Execute()
    {
        var dir = _behaviour.GetDir();
        _onChase?.Invoke(dir);
        _counter -= Time.deltaTime;

        Debug.LogError("ChaseState");

        if (_counter > 0) return;

        _root.Execute();
        ResetCounter();
    }
}