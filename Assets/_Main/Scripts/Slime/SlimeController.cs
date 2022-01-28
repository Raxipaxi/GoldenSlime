using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SlimeController:MonoBehaviour
{
    #region properties
    private FSM<EnemyStates> _fsm;
    private SlimeModel _slime;
    #endregion

    public event Action<Vector3> OnMove;
    public event Action OnIdle;

    private void Awake()
    {
        _slime = GetComponent<SlimeModel>();
        FsmInit();
    }

    private void Start()
    {
        _slime.Suscribe(this);
    }
    private void FsmInit()
    {

    }
    public void WalkCommand(Vector3 dir)
    {

    }
}
