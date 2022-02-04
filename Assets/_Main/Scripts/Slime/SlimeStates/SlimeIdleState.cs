using System.Collections;
using UnityEngine;
using System;
public class SlimeIdleState<T> : State<T>
{
    private Action _onIdle;
    public SlimeIdleState(Action idle)
    {
      
    }

    public override void Execute()
    {
        Debug.Log("Idle");
        _onIdle.Invoke();
    }
}
