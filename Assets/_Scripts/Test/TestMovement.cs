using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    
    private float movementDirection;
    
    private void Awake()
    {
        Physics.gravity = new Vector3(0, -MathFunctions.G, 0);
    }
    
    private void FixedUpdate()
    {
        AddMovement();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Globals.PLATFORM_TAG) && other.transform.position.y < transform.position.y)
            EventManager.RaiseOnPlayerPlatformHit(other.gameObject.GetComponent<Platform>().PlatformID);
    }
    
    private void AddMovement()
    {
        playerRigidbody.linearVelocity = new Vector3(movementDirection * movementSpeed, playerRigidbody.linearVelocity.y, playerRigidbody.linearVelocity.z);
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
            return;
        
        movementDirection = context.ReadValue<float>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            return;
        
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, jumpForce, playerRigidbody.linearVelocity.z);
    }
}