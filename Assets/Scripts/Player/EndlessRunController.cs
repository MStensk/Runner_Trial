using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EndlessRunController : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 8f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float groundedStickForce = -0.5f;
    [SerializeField] private float maxFallSpeed = -20f;

    [Header("Lane Movement")]
    [SerializeField] private float laneOffset = 2.5f;
    [SerializeField] private float laneChangeSpeed = 5f;

    [Header("Turning")]
    [SerializeField] private bool allowManualTurnDebug = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private CharacterController controller;

    // -2 = far left, 0 = center, 2 = far right
    private int currentLane = 0;

    // Movement directions relative to current track
    private Vector3 forwardDirection;
    private Vector3 rightDirection;

    // Lane anchor
    private Vector3 currentLaneCenter;

    // Gravity
    private float verticalVelocity;

    // Turn permissions
    private bool canTurnLeft;
    private bool canTurnRight;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        currentLaneCenter = transform.position;
        currentLaneCenter.y = transform.position.y;

        verticalVelocity = groundedStickForce;
    }

    private void Update()
    {
        HandleLaneInput();
        HandleTurnInput();
        MovePlayer();
        UpdateAnimation();
    }

    private void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(currentLane - 2, -2);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(currentLane + 2, 2);
    }

    private void HandleTurnInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (canTurnLeft || allowManualTurnDebug))
            TurnLeft();

        if (Input.GetKeyDown(KeyCode.X) && (canTurnRight || allowManualTurnDebug))
            TurnRight();
    }

    private void MovePlayer()
    {
        // Move the lane center forward
        currentLaneCenter += forwardDirection * forwardSpeed * Time.deltaTime;

        // Lane target
        Vector3 targetPosition = currentLaneCenter + rightDirection * (currentLane * laneOffset);

        // Flatten positions so lane movement does not affect Y
        Vector3 currentPosition = transform.position;
        Vector3 flatCurrent = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 flatTarget = new Vector3(targetPosition.x, 0f, targetPosition.z);

        // Smooth horizontal move
        Vector3 horizontalMove = (flatTarget - flatCurrent) * laneChangeSpeed;

        // Gravity / ground stick
        if (controller.isGrounded)
        {
            verticalVelocity = groundedStickForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, maxFallSpeed);
        }

        Vector3 move = horizontalMove;
        move += forwardDirection * forwardSpeed;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private void TurnLeft()

    {
        transform.Rotate(0f, -90f, 0f);

        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        currentLaneCenter = transform.position - rightDirection * (currentLane * laneOffset);
        currentLaneCenter.y = transform.position.y;

        Vector3 snappedPosition = currentLaneCenter + rightDirection * (currentLane * laneOffset);
        snappedPosition.y = transform.position.y;
        transform.position = snappedPosition;

        canTurnLeft = false;
        canTurnRight = false;
    }

    private void TurnRight()
    
    {
        transform.Rotate(0f, 90f, 0f);

        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        currentLaneCenter = transform.position - rightDirection * (currentLane * laneOffset);
        currentLaneCenter.y = transform.position.y;

        Vector3 snappedPosition = currentLaneCenter + rightDirection * (currentLane * laneOffset);
        snappedPosition.y = transform.position.y;
        transform.position = snappedPosition;

        canTurnLeft = false;
        canTurnRight = false;
    }

    public void SetTurnAvailability(bool leftAllowed, bool rightAllowed)
    {
        canTurnLeft = leftAllowed;
        canTurnRight = rightAllowed;
    }

    public void ClearTurnAvailability()
    {
        canTurnLeft = false;
        canTurnRight = false;
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", forwardSpeed);
    }
}