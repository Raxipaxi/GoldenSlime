using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SlimeController : MonoBehaviour
{
    #region properties
    private FSM<EnemyStates> _fsm;
    private SlimeModel _slimeModel;
    [SerializeField] private PlayerModel _target;

    [SerializeField] private ObstacleAvoidanceSO avoidtats;
    private INode _root;

    public ObstacleAvoidance behaviour;
    #endregion

    //ublic event Action<Vector3> OnMove;
    public event Action<Vector3> OnWalk;
    public event Action OnIdle;
    public event Action<Vector3> OnRun;
    public event Action OnAttack;
    public event Action OnDie, OnHit;

    private void Awake()
    {
        _slimeModel = GetComponent<SlimeModel>();
        behaviour = new ObstacleAvoidance(transform, _target.transform, avoidtats.CheckRadius, avoidtats.MaxObj, avoidtats.ObstacleLayer, avoidtats.Multiplier, _target.Vel, avoidtats.TimePrediction, ObstacleAvoidance.Steering.Wander);
        

    }

    private void Start()
    {
        _slimeModel.OnDie += DieCommand;
        _slimeModel.Suscribe(this);
        InitDecisionTree();
        FsmInit();  
    }
    #region Commands
    private void WalkCommand(Vector3 moveDir)
    {
        OnWalk?.Invoke(moveDir);
    }
    private void IdleCommand()
    {
        OnIdle.Invoke();
    }
    public void DieCommand()
    {
        _fsm.Transition(EnemyStates.Patrol);
    }
    private void RunCommand(Vector3 dir)
    {
        OnRun?.Invoke(dir);
    }
    #endregion
    private void FsmInit()
    {
        var patrol = new SlimePatrolState<EnemyStates>(CanSeeTarget, WalkCommand, _root,_target.transform, behaviour);
        var dead = new SlimeDieState<EnemyStates>(DieCommand);
        var idle = new SlimeIdleState<EnemyStates>(IdleCommand);
        var chase = new SlimeChaseState<EnemyStates>(_target.transform, _root, behaviour, RunCommand, _slimeModel._stats.AttackCooldown);

        idle.AddTransition(EnemyStates.Patrol, patrol);

        patrol.AddTransition(EnemyStates.Follow, chase);

        chase.AddTransition(EnemyStates.Patrol, patrol);
        dead.AddTransition(EnemyStates.Idle, idle);

        _fsm = new FSM<EnemyStates>();
        _fsm.SetInit(patrol);
    }
    private void InitDecisionTree()
    {
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStates.Idle));
        var goToPatrol = new ActionNode(() => _fsm.Transition(EnemyStates.Patrol));
        var goToAttack = new ActionNode(() => _fsm.Transition(EnemyStates.Attack));
        var goToChase = new ActionNode(() => _fsm.Transition(EnemyStates.Follow));
        var goToScape = new ActionNode(() => _fsm.Transition(EnemyStates.Escape));


        QuestionNode isPlayerAlive = new QuestionNode(_target.IsAlive, goToPatrol, goToIdle);
        QuestionNode isOnReach = new QuestionNode(CanAttack, goToAttack, goToChase);
        QuestionNode isDayTime = new QuestionNode(DayTime, goToScape, isOnReach);
        QuestionNode isOnPlayerSight = new QuestionNode(CanSeeTarget, goToChase, goToPatrol);
        QuestionNode isRecentilySpawned = new QuestionNode(IsRecentlySpawned, isOnPlayerSight, null);

        _root = isPlayerAlive;
    }

    private bool CanAttack()
    {

        return CanAttack();
    }
    private bool DayTime()
    {
        return DayTime();
    }
    private bool IsRecentlySpawned()
    {
        return true;
    }
    public bool CanSeeTarget()
    {
        var playerInSight = _slimeModel.LineOfSight.CanSeeSomeone(_target.transform);
        return playerInSight;
    }
    private void Update()
    {
        _fsm.OnUpdate();
    }
}
