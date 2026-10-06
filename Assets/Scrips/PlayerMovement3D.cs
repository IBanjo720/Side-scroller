using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;

    [Header("Gravity")]
    [SerializeField] private float extraGravity = 20f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Keep the game 3D, but lock the player
        // to the 2D-style gameplay plane.
        rb.constraints =
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        CheckGround();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();

        // Extra gravity while falling.
        // This makes the player come down faster.
        if (rb.velocity.y < 0f)
        {
            rb.AddForce(
                Vector3.down * extraGravity,
                ForceMode.Acceleration
            );
        }
    }

    private void Move()
    {
        float input = 0f;

        // A = move left
        if (Input.GetKey(KeyCode.A))
        {
            input = -1f;
        }

        // D = move right
        if (Input.GetKey(KeyCode.D))
        {
            input = 1f;
        }

        Vector3 velocity = rb.velocity;

        // Z is the left/right direction.
        velocity.z = input * moveSpeed;

        // Keep vertical velocity.
        velocity.y = rb.velocity.y;

        // X is locked.
        velocity.x = 0f;

        rb.velocity = velocity;
    }

    private void Jump()
    {
        Vector3 velocity = rb.velocity;

        // Remove downward velocity before jumping.
        if (velocity.y < 0f)
        {
            velocity.y = 0f;
            rb.velocity = velocity;
        }

        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );
    }

    private void CheckGround()
    {
        Vector3 origin = transform.position;

        isGrounded = Physics.Raycast(
            origin,
            Vector3.down,
            groundCheckDistance + 0.5f,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 start = transform.position;

        Gizmos.DrawLine(
            start,
            start + Vector3.down * (groundCheckDistance + 0.5f)
        );
    }
}


