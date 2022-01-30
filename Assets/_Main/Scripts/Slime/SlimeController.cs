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
    [SerializeField] private float chaseCD; 
    [SerializeField] private float evadeCD;
    [SerializeField] private Daylight _daylight;


    private bool dayNight;
    
    
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
        SubscribeEvents();
        
        _slimeModel.Suscribe(this);
        InitDecisionTree();
        FsmInit();  
    }

    void SubscribeEvents()
    {
        _slimeModel.OnDie += DieCommand;
        _daylight.OnDay += () => dayNight = true;
        _daylight.OnNight += () => dayNight = false;
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
        // States
        var patrol = new SlimePatrolState<EnemyStates>(CanSeeTarget, WalkCommand, _root,_target.transform, behaviour);
        var dead = new SlimeDieState<EnemyStates>(DieCommand);
        var idle = new SlimeIdleState<EnemyStates>(IdleCommand);
        var chase = new SlimeChaseEvadeState<EnemyStates>(_target.transform, RunCommand, CanSeeTarget, behaviour,
           ObstacleAvoidance.Steering.Chase, chaseCD, _root);
        var evade = new SlimeChaseEvadeState<EnemyStates>(_target.transform, RunCommand, CanSeeTarget, behaviour,
           ObstacleAvoidance.Steering.Flee, evadeCD, _root);
        
       
        // Idle
        idle.AddTransition(EnemyStates.Patrol, patrol);
        idle.AddTransition(EnemyStates.Chase, chase);
        idle.AddTransition(EnemyStates.Evade, evade);
        idle.AddTransition(EnemyStates.Die,dead);

        
        // Patrol
        patrol.AddTransition(EnemyStates.Chase, chase);
        patrol.AddTransition(EnemyStates.Evade, evade);
        patrol.AddTransition(EnemyStates.Idle, idle);
        patrol.AddTransition(EnemyStates.Die, dead);

        
        // Chase
        chase.AddTransition(EnemyStates.Patrol, patrol);
        chase.AddTransition(EnemyStates.Evade, evade);
        chase.AddTransition(EnemyStates.Idle, idle);
        chase.AddTransition(EnemyStates.Die, dead);
        
        
        // Evade
        evade.AddTransition(EnemyStates.Patrol, patrol);
        evade.AddTransition(EnemyStates.Chase, chase);
        evade.AddTransition(EnemyStates.Idle, idle);
        evade.AddTransition(EnemyStates.Die, dead);
        

        _fsm = new FSM<EnemyStates>();
        _fsm.SetInit(patrol);
    }
    private void InitDecisionTree()
    {
        // Actions
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStates.Idle));
        var goToPatrol = new ActionNode(() => _fsm.Transition(EnemyStates.Patrol));
        var goToAttack = new ActionNode(() => _fsm.Transition(EnemyStates.Attack));
        var goToChase = new ActionNode(() => _fsm.Transition(EnemyStates.Follow));
        var goToScape = new ActionNode(() => _fsm.Transition(EnemyStates.Evade));

        
        // Questions
        
        QuestionNode isOnReach = new QuestionNode(CanAttack, goToAttack, goToChase);
        QuestionNode isDayTime = new QuestionNode(DayTime, goToScape, isOnReach);
        QuestionNode isPlayerSight = new QuestionNode(CanSeeTarget, isDayTime, goToPatrol);
        QuestionNode isPlayerAlive = new QuestionNode(PlayerAlive, isPlayerSight, goToPatrol);
        QuestionNode isRecentilySpawned = new QuestionNode(IsRecentlySpawned, isPlayerSight, null);

        _root = isPlayerAlive;
    }

    private bool CanAttack()
    {

        return CanAttack();
    }
    private bool DayTime()
    {
        return dayNight;
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

    public bool PlayerAlive()
    {
        return _target.IsAlive();
    }
    private void Update()
    {
        _fsm.OnUpdate();
    }
}
