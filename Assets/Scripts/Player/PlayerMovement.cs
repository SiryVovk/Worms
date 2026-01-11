using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int jumpPrice = 10;
    [SerializeField] private int moveEnergyCost = 1;
    [SerializeField] private int maxEnergy = 100;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private float movementDirection;
    private int currentEnergy;
    private bool IsGrounded = true;

    private const string GROUND_TAG = "Ground";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.OnMoveAction += HandleMove;
        playerInput.OnJumpAction += HandleJump;

        currentEnergy = maxEnergy;
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
        if (IsGrounded && currentEnergy >= jumpPrice)
        {
            currentEnergy -= jumpPrice;
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
        if(Mathf.Abs(movementDirection) > 0.1f && currentEnergy >= moveEnergyCost)
        {
            currentEnergy -= moveEnergyCost;
        }
        else
        {
            movementDirection = 0f;
        }
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public int GetMaxEnergy()
    {
        return maxEnergy;
    }

    public void RechargeEnergy(int amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            IsGrounded = true;
        }
    }
}
