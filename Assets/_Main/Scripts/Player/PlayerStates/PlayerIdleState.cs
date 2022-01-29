using System;

public class PlayerIdleState<T> : State<T>
{
    T _inputWalk;
    T _inputMelee;
    private Action _onIdle;
    private iInput _playerInput;

    public PlayerIdleState(Action onIdle, T inputWalk, T inputMelee, iInput playerInput)
    {
        _inputWalk = inputWalk;
        _inputMelee = inputMelee;
        _onIdle = onIdle;
        _playerInput = playerInput;
    }


    public override void Execute()
    {
        _playerInput.UpdateInputs();
        if (_playerInput.IsMoving())
        {
            _fsm.Transition(_inputWalk);
            return;
        }

        if (_playerInput.IsAttackMelee())
        {
            _fsm.Transition(_inputMelee);
        }
        _onIdle?.Invoke();
    }
}
