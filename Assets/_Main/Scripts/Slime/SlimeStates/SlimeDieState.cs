using System;
using System.Collections.Generic;
using UnityEngine;
class SlimeDieState<T> : State<T>
{

    private Action _onDead;
    public SlimeDieState(Action onDead)
    {
        _onDead = onDead;
    }

    public override void Awake()
    {
        _onDead?.Invoke();
    }
}

