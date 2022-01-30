using System.Collections;
using UnityEngine;
using System;
public class SlimeAttackState<T> : State<T>
{
    private INode _root;
    private Action<float> _onAttack;
    private float _counter;
    private float _damage;
    private float _timeToAttack;

    public SlimeAttackState(Action<float> onAttack, float timeToAttack, float damage ,INode root)
    {
        _onAttack = onAttack;
        _damage = damage;
        _timeToAttack = timeToAttack;
        _root = root;

    }
    public override void Awake()
    {
        ResetCounter();
        _onAttack?.Invoke(_damage);
    }
    private void ResetCounter()
    {
        _counter = _timeToAttack;
    }
    public override void Execute()
    {
        _counter -= Time.deltaTime;
        
        if (_counter > 0) return;
  
        _root.Execute();
    }
}
