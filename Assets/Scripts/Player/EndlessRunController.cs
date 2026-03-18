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
    [SerializeField] private float laneOffset = 5f; // match your wide lanes
    [SerializeField] private float laneChangeSpeed = 5f;

    [Header("Turning")]
    [SerializeField] private bool allowManualTurnDebug = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private CharacterController controller;

    // -1 = left, 0 = center, 1 = right
    private int currentLane = 0;

    private Vector3 forwardDirection;
    private Vector3 rightDirection;
    private Vector3 currentLaneCenter;

    private float verticalVelocity;

    private bool canTurnLeft;
    private bool canTurnRight;

    // Turn lane targets
    private Transform turnLaneLeft;
    private Transform turnLaneCenter;
    private Transform turnLaneRight;

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

        currentLaneCenter = transform.position - rightDirection * (currentLane * laneOffset);
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
            currentLane = Mathf.Max(currentLane - 1, -1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(currentLane + 1, 1);
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
        currentLaneCenter += forwardDirection * forwardSpeed * Time.deltaTime;

        Vector3 targetPosition = currentLaneCenter + rightDirection * (currentLane * laneOffset);

        Vector3 currentPosition = transform.position;
        Vector3 flatCurrent = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 flatTarget = new Vector3(targetPosition.x, 0f, targetPosition.z);

        Vector3 horizontalMove = (flatTarget - flatCurrent) * laneChangeSpeed;

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

        SnapToClosestLane();

        canTurnLeft = false;
        canTurnRight = false;
    }

    private void TurnRight()
    {
        transform.Rotate(0f, 90f, 0f);

        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        SnapToClosestLane();

        canTurnLeft = false;
        canTurnRight = false;
    }

    private void SnapToClosestLane()
    {
        // If no lane targets assigned → fallback
        if (turnLaneCenter == null)
        {
            currentLaneCenter = transform.position - rightDirection * (currentLane * laneOffset);
            currentLaneCenter.y = transform.position.y;

            Vector3 fallback = currentLaneCenter + rightDirection * (currentLane * laneOffset);
            fallback.y = transform.position.y;
            transform.position = fallback;
            return;
        }

        Transform[] lanes = new Transform[] { turnLaneLeft, turnLaneCenter, turnLaneRight };

        float closestDistance = Mathf.Infinity;
        Transform closestLane = turnLaneCenter;
        int closestLaneIndex = 0;

        for (int i = 0; i < lanes.Length; i++)
        {
            if (lanes[i] == null) continue;

            float dist = Vector3.Distance(transform.position, lanes[i].position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestLane = lanes[i];
                closestLaneIndex = i - 1; // converts 0,1,2 → -1,0,1
            }
        }

        // Snap player
        Vector3 snappedPosition = closestLane.position;
        snappedPosition.y = transform.position.y;
        transform.position = snappedPosition;

        // Update lane system
        currentLane = closestLaneIndex;

        currentLaneCenter = turnLaneCenter.position;
        currentLaneCenter.y = transform.position.y;
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

    public void SetTurnLaneTargets(Transform left, Transform center, Transform right)
    {
        turnLaneLeft = left;
        turnLaneCenter = center;
        turnLaneRight = right;
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        animator.SetFloat("Speed", forwardSpeed);
    }
}