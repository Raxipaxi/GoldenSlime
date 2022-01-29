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

    public event Action<Vector3> OnMove;
    public event Action OnPatrol;
    public event Action OnChase;
    public event Action OnAttack;
    public event Action OnDie, OnHit;

    private void Awake()
    {
        _slimeModel = GetComponent<SlimeModel>();
        behaviour = new ObstacleAvoidance(transform, _target.transform, avoidtats.CheckRadius, avoidtats.MaxObj, avoidtats.ObstacleLayer, avoidtats.Multiplier, _target.Vel, avoidtats.TimePrediction, ObstacleAvoidance.Steering.Wander);
        

    }

    private void Start()
    {
        _slimeModel.Suscribe(this);
        FsmInit();  
        InitDecisionTree();
    }
    #region Commands
    private void WalkCommand(Vector3 moveDir)
    {
        OnMove?.Invoke(moveDir);
    }
    private void PatrolCommand()
    {
        OnPatrol.Invoke();
    }
    #endregion
    private void FsmInit()
    {
        var patrol = new SlimePatrolState<EnemyStates>(CanSeeTarget, WalkCommand, OnPatrol , _root,_target.transform, behaviour);
        _fsm = new FSM<EnemyStates>();
        _fsm.SetInit(patrol);
    }
    private void InitDecisionTree()
    {
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStates.Patrol));
        var goToAttack = new ActionNode(() => _fsm.Transition(EnemyStates.Attack));
        var goToChase = new ActionNode(() => _fsm.Transition(EnemyStates.Follow));
        var goToScape = new ActionNode(() => _fsm.Transition(EnemyStates.Escape));

        QuestionNode isOnReach = new QuestionNode(CanAttack, goToAttack, goToChase);
        QuestionNode isDayTime = new QuestionNode(DayTime, goToScape, isOnReach);
        QuestionNode isOnPlayerSight = new QuestionNode(CanSeeTarget, isDayTime, goToIdle);
    }

    private bool CanAttack()
    {

        return CanAttack();
    }
    private bool DayTime()
    {
        return DayTime();
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
