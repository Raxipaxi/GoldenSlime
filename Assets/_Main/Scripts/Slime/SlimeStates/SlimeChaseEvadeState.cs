using System.Collections;
using UnityEngine;
using System;
using UnityEditor.VersionControl;

public class SlimeChaseEvadeState<T> : State<T>
{
    private Action<Vector3>  _behave;
    private T _inputWander;
    private INode _root;
    private ObstacleAvoidance _obst;
    private ObstacleAvoidance.Steering _steering;
    private Transform _target;
    private float _counter;
    private float _timeToAct;
    
    public SlimeChaseEvadeState(Transform target,Action<Vector3> behave, ObstacleAvoidance obst, ObstacleAvoidance.Steering steering,float timeToAct, INode root)
    {
        _behave = behave;
        _root = root;
        _obst = obst;
        _steering = steering;
        _target = target;
        _timeToAct = timeToAct;
    }
    

    public override void Awake()
    {
        _obst.SetNewBehaviour(_steering);
        _obst.SetNewTarget(_target);
        ResetCounter();
    }
    private void ResetCounter()
    {
        _counter = Time.time+_timeToAct;
    }

    public override void Execute()
    {

        if (_steering==ObstacleAvoidance.Steering.Chase)
        {
            Debug.Log(_steering);
        }

    
        var dir = _obst.GetDir();
        _behave?.Invoke(dir);
        
        if (_counter < Time.time) 
        {  
            
            _root.Execute();
            return;
        }
        
    }
}
