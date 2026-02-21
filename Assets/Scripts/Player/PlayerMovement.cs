using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int jumpPrice = 10;
    [SerializeField] private int moveEnergyCost = 1;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerEnergy playerEnergy;

    private float movementDirection;
    private bool IsGrounded = true;

    private const string GROUND_TAG = "Ground";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerEnergy = GetComponent<PlayerEnergy>();

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
        if (IsGrounded && playerEnergy.HasEnoughEnergy(jumpPrice))
        {
            playerEnergy.UseEnergy(jumpPrice);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            IsGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        rb.linearVelocity = new Vector2(movementDirection * moveSpeed, rb.linearVelocity.y);
    }

    private void Move()
    {
        if(Mathf.Abs(movementDirection) > 0.1f && playerEnergy.HasEnoughEnergy(moveEnergyCost))
        {
            playerEnergy.UseEnergy(moveEnergyCost);
        }
        else
        {
            movementDirection = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            IsGrounded = true;
        }
    }
}
