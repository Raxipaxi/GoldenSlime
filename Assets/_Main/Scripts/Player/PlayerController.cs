using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Properties

    private FSM<PlayerStatesEnum> _fsm;
    private PlayerModel _player;
    private iInput _playerInput;
    #endregion

    public event Action<Vector3> OnMove; 
    public event Action OnIdle;
    public event Action<float> OnMelee;
    private void Awake()
    {
        _player = GetComponent<PlayerModel>();
        _playerInput = GetComponent<iInput>();
        
        FsmInit();
    }

    private void Start()
    {
        _player.Subscribe(this);
    }

    private void FsmInit()
    {
        
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(IdleCommmand, PlayerStatesEnum.Walk,PlayerStatesEnum.Melee,_playerInput );
        var walk = new PlayerWalkState<PlayerStatesEnum>(WalkCommmand, PlayerStatesEnum.Idle, PlayerStatesEnum.Melee, _playerInput);
        var melee = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Walk,AttMeleeCommand,1,_playerInput);
        
        
        // Idle
        idle.AddTransition(PlayerStatesEnum.Walk, walk);
        idle.AddTransition(PlayerStatesEnum.Melee, melee);
        
        // Walk
        walk.AddTransition(PlayerStatesEnum.Idle, idle);
        walk.AddTransition(PlayerStatesEnum.Melee, melee);
        
        // Melee
        melee.AddTransition(PlayerStatesEnum.Idle, idle);
        melee.AddTransition(PlayerStatesEnum.Walk, walk);

        _fsm = new FSM<PlayerStatesEnum>();
        // Set init state
        _fsm.SetInit(idle);

    }

    public void WalkCommmand(Vector3 dir)
    {
        OnMove?.Invoke(dir);
    }
    public void IdleCommmand()
    {
        OnIdle?.Invoke();
    }

    public void AttMeleeCommand(float dmg)
    {
        OnMelee?.Invoke(dmg);   
    }
    
    void Update()
    {
        _fsm.OnUpdate();   
    }


}

