using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("References")]
    [SerializeField] private Transform modelPivot; // The 3D model's parent pivot

    private Rigidbody rb;
    private float horizontalInput;
    private bool isFacingRight = true;
    private Vector3 originalScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = modelPivot.localScale;

        // Lock Z position and rotation for side-scroller
        rb.constraints = RigidbodyConstraints.FreezePositionZ |  // Lock Z-axis movement
                    RigidbodyConstraints.FreezeRotationX |   // Prevent tilting forward/backward
                    RigidbodyConstraints.FreezeRotationY |   // Prevent spinning
                    RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        GetInput();
        HandleRotation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void Move()
    {
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;
    }

    void HandleRotation()
    {
        if (modelPivot == null) return;

        // Flip model based on input direction
        if (horizontalInput > 0.1f && !isFacingRight)
        {
            isFacingRight = true;
            modelPivot.localScale = originalScale;
        }
        else if (horizontalInput < -0.1f && isFacingRight)
        {
            isFacingRight = false;
            modelPivot.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        // Optional: Smooth rotation instead of flipping
        // if (horizontalInput != 0)
        // {
        //     float targetAngle = isFacingRight ? 0 : 180;
        //     Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        //     modelPivot.rotation = Quaternion.Lerp(modelPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // }
    }
}