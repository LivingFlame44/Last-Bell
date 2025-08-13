using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float maxVelocity = 7f;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private float horizontalInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get input (only A and D keys)
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        // Update animator
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Flip sprite based on movement direction
        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ClampVelocity();
    }

    private void HandleMovement()
    {
        Vector3 targetVelocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);

        // Apply acceleration/deceleration
        if (horizontalInput != 0)
        {
            // Accelerate
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, rb.velocity.y, 0), deceleration * Time.fixedDeltaTime);
        }
    }

    private void ClampVelocity()
    {
        // Limit maximum velocity
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, 0);
        if (horizontalVelocity.magnitude > maxVelocity)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxVelocity;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, 0);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}