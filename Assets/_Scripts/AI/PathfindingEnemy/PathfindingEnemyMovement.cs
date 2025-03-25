using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(5)]
public class PathfindingEnemyMovement : MonoBehaviour
{
    
    #region PROPERTIES
    
    public bool IsGrounded { get; set; }
    public int CurrentPlatformID { get; set; }
    
    #endregion
    
    #region VARIABLES
    
    [SerializeField] private Rigidbody enemyRigidbody;
    
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
    
    private List<Node> calculatedPath = new List<Node>();
    private int currentPathNodeIndex;
    private bool isPathCalculated;

    private Vector3 movementDirection;

    private readonly float nodeDistanceBuffer = 0.1f;
    
    #endregion


    private void Start()
    {
        _ = FindPath(transform.position);
    }

    #region METHODS
    
    public async Task SetUpMove()
    {
        Vector3 finalPosition = transform.position;

        if (!isPathCalculated)
            return;

        if (!IsGrounded && isPathCalculated)
            finalPosition = calculatedPath[currentPathNodeIndex].Position;
        
        await FindPath(finalPosition);
    }
    
    private async Task FindPath(Vector3 finalPosition)
    {
        isPathCalculated = false;
        currentPathNodeIndex = 0;

        calculatedPath = await Pathfinding.Instance.FindPathAsync(finalPosition, EnemyManager.Instance.currentPlayerPlatformID, EnemyManager.Instance.player.transform.position);

        isPathCalculated = true;
    }
    
    
    public void Move()
    {
        if (!isPathCalculated || !IsGrounded)
            return;
        
        if (IsAtNode())
            CalculateMovementToNextNode();
        else
            ApplyMovementDirectionTo(calculatedPath[currentPathNodeIndex].Position);
    }
    
    private bool IsAtNode()
    {
        return Vector3.Distance(transform.position, calculatedPath[currentPathNodeIndex].Position) < nodeDistanceBuffer;
    }

    private void CalculateMovementToNextNode()
    {
        currentPathNodeIndex++;
        
        if (IsNextNodeOnTheSamePlatform())
            ApplyMovementDirectionTo(calculatedPath[currentPathNodeIndex].Position);
        else
            Jump();
    }
    
    private bool IsNextNodeOnTheSamePlatform()
    {
        return calculatedPath[currentPathNodeIndex].ParentParentPlatformID == CurrentPlatformID;
    }

    
    private void ApplyMovementDirectionTo(Vector3 targetPosition)
    {
        CalculateSidewaysMovementExcludingGravity(transform.position, targetPosition);
        enemyRigidbody.linearVelocity = new Vector3(movementDirection.x * walkingSpeed, enemyRigidbody.linearVelocity.y, 0f);
    }

    private void Jump()
    {        
        if (IsNextNodeUp())
            JumpUp();
        else
            JumpDown();
        
        IsGrounded = false;
        movementDirection = Vector3.zero;
    }
    
    private void JumpUp()
    {
        Vector3 calculatedJumpVelocity = MathFunctions.CalculateProjectileVelocityUp(transform.position, calculatedPath[currentPathNodeIndex].Position);
        enemyRigidbody.linearVelocity = calculatedJumpVelocity;
    }
    
    private void JumpDown()
    {
        Vector3 calculatedJumpVelocity = MathFunctions.CalculateProjectileVelocityDown(transform.position, calculatedPath[currentPathNodeIndex].Position);
        enemyRigidbody.linearVelocity = calculatedJumpVelocity;
    }

    private bool IsNextNodeUp()
    {
        return calculatedPath[currentPathNodeIndex - 1].Position.y < calculatedPath[currentPathNodeIndex].Position.y;
    }
    
    public void Chase()
    {
        if (!IsGrounded)
            return;
        
        CalculateSidewaysMovementDirectionIncludingGravity(transform.position, EnemyManager.Instance.player.transform.position);
        enemyRigidbody.linearVelocity = movementDirection;
    }

    private void CalculateSidewaysMovementExcludingGravity(Vector3 startingPosition, Vector3 targetPosition)
    {
        movementDirection.x = (targetPosition.x - startingPosition.x).Normalize();
        movementDirection.y = 0f;
        movementDirection.z = 0f;
    }

    private void CalculateSidewaysMovementDirectionIncludingGravity(Vector3 startingPosition, Vector3 targetPosition)
    {
        float xDirection =  (targetPosition.x - startingPosition.x).Normalize();
        movementDirection.x = xDirection * chasingSpeed;
        movementDirection.y = enemyRigidbody.linearVelocity.y;
    }
    
    #endregion
    
    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if (calculatedPath != null && calculatedPath.Count != 0 && CurrentPlatformID != EnemyManager.Instance.currentPlayerPlatformID)
        {
            Gizmos.color = Color.red;
            for (int i = currentPathNodeIndex; i < calculatedPath.Count; i++)       
                Gizmos.DrawSphere(calculatedPath[i].Position, .3f);
        }
    }
    
    #endif
    
}
