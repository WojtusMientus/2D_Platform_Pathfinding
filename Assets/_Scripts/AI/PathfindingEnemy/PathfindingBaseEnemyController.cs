using System;
using System.Collections;
using UnityEngine;


public class PathfindingEnemyController : BaseEnemyController
{
    
    #region VARIABLES
    
    private readonly MoveState pathfindingState = new MoveState();
    private readonly ChaseState samePlatformChasingBaseState = new ChaseState();
    private readonly AttackState attackBaseState = new AttackState();
    
    [SerializeField] private PathfindingEnemyMovement movementComponent;
    
    [Space(10)]
    [SerializeField] private float attackRange = 1f;
    private readonly float attackRangeBuffer = 0.3f;
    
    private bool isAttacking;
    
    #endregion
    
    
    
    private  void Awake() 
    {
        InitializeStates();
    }

    private void Start()
    {
        SwitchState(pathfindingState);
    }

    private void OnEnable()
    {
        EventManager.OnPlayerPlatformHit += AdjustBehaviorBasedOnPlayerNewPlatform;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerPlatformHit -= AdjustBehaviorBasedOnPlayerNewPlatform;
    }
    
    private void FixedUpdate()
    {
        currentState.UpdateState();
    }
    
    
    #region METHODS
    
    protected override void InitializeStates()
    {
        pathfindingState.InitializeState(this);
        samePlatformChasingBaseState.InitializeState(this);
        attackBaseState.InitializeState(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Globals.PLATFORM_TAG))
        {
            movementComponent.CurrentPlatformID = other.gameObject.GetComponentInParent<Platform>().PlatformID;
            movementComponent.IsGrounded = true;

            ChooseState();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(Globals.PLATFORM_TAG))
            movementComponent.IsGrounded = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Globals.PLAYER_TAG) && movementComponent.IsGrounded)
            SwitchState(attackBaseState); 
    }
    
    private void ChooseState()
    {
        if (IsInAttackRangeAndOnTheSamePlatform())
            SwitchState(attackBaseState);
        
        else if (IsOnTheSamePlatformAsPlayer())
            SwitchState(samePlatformChasingBaseState);
        
        else if (!isAttacking)
            SwitchState(pathfindingState);
    }
    
    private bool IsInAttackRangeAndOnTheSamePlatform() => GetHorizontalDistanceToPlayer() <= attackRange + attackRangeBuffer && IsOnTheSamePlatformAsPlayer();

    private float GetHorizontalDistanceToPlayer() =>
        Mathf.Abs(transform.position.x - EnemyManager.Instance.player.transform.position.x);

    private bool IsOnTheSamePlatformAsPlayer() =>
        movementComponent.CurrentPlatformID == EnemyManager.Instance.currentPlayerPlatformID;
    
    public override void SetupMove()
    {
        _ = movementComponent.SetUpMove();
    }

    public override void Move()
    {
        movementComponent.Move();
    }
    
    public override void Chase()
    {
        movementComponent.Chase();
    }
    
    public override void Attack()
    {
        StartCoroutine(WaitAndResumeBehaviour());
    }
    
    private IEnumerator WaitAndResumeBehaviour()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        
        ChangeStateAfterAttack();
    }

    private void ChangeStateAfterAttack()
    {
        isAttacking = false;
        
        ChooseState();
    }
    
    private void AdjustBehaviorBasedOnPlayerNewPlatform(int playerPlatformID)
    {
        if (isAttacking)
            return;
        
        if (playerPlatformID == movementComponent.CurrentPlatformID)
            SwitchState(samePlatformChasingBaseState);
        else
            SwitchState(pathfindingState);
    }
    
    #endregion
    
    #if UNITY_EDITOR
    
    private SphereCollider sphereTrigger;
    
    private void OnValidate()
    {
        if (sphereTrigger == null)
            sphereTrigger = GetComponent<SphereCollider>();
        
        sphereTrigger.radius = attackRange;
    }

#endif
    
}
