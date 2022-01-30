using System;
using UnityEngine;


public class PlayerAttackState<T> : State<T>
{
    private float _dmg;
    private T _inputIdle;
    private T _inputMove;
    private iInput _playerInput;
    private Action<float> _onAttack;

    public PlayerAttackState(T inputIdle, T inputMove, Action<float> onAttack,float dmg, iInput playerInput)
    {
        _onAttack = onAttack;
        _inputIdle = inputIdle;
        _inputMove = inputMove;
        _dmg = dmg;
        _playerInput = playerInput;
    }

    public override void Awake()
    {
        Debug.Log("PEW PEW PEW");
    }

    public override void Execute()
    {
        _onAttack?.Invoke(_dmg);
        _playerInput.UpdateInputs();
        Debug.Log("Pew pew");
        if (_playerInput.IsMoving())
        {
            _fsm.Transition(_inputMove);
        }
        else
        {
            _fsm.Transition(_inputIdle);
        }
        
        
    }
}
