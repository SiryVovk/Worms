using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private float movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.OnMoveAction += HandleMove;
        playerInput.OnJumpAction += HandleJump;
    }

    private void OnDestroy()
    {
        playerInput.OnMoveAction -= HandleMove;
        playerInput.OnJumpAction -= HandleJump;
    }

    private void HandleMove(float inputDirection)
    {
        movementDirection = inputDirection;
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementDirection * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}
