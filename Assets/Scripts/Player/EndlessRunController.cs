
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;

[RequireComponent(typeof(CharacterController))]
public class EndlessRunController : MonoBehaviour
{
   private bool useManualTestMovement = true;
    [SerializeField] private float testSideStep = 1.2f;
    [SerializeField] private float testForwardStep = 5.0f;

    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 8f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float groundedStickForce = 0f;
    
    // maxFallSpeed good test on -50f
    [SerializeField] private float maxFallSpeed = -50f;
    [SerializeField] private float maxSpeed = 30f;

    [Header("Score Multiplier")]
    [SerializeField] private int coinScoreMultiplier = 1;
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private int coinsPerMultiplierStep = 10;
    [SerializeField] private int maxCoinScoreMultiplier = 10;

    [Header("Lane Movement")]
    [SerializeField] private float laneOffset = 5f;
    [SerializeField] private float laneChangeSpeed = 5f;

    [Header("Turning")]
    [SerializeField] private bool allowManualTurnDebug = false;
    [SerializeField] private bool haveJumped = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Jump")]

    // jumpForce was 2.5f
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private float fallMultiplier = 2f;

    [Header("Slide")]
    [SerializeField] private bool isSliding = false;
    [SerializeField] private float slideDuration = 0.8f;

    float maxJump = 3.8f;
    
    private CharacterController controller;

    private int currentLane = 0;
    private Vector3 forwardDirection;
    private Vector3 rightDirection;
    private Vector3 currentLaneCenter;
    private float verticalVelocity;
    private bool canTurnLeft;
    private bool canTurnRight;
    private Transform turnLaneLeft;
    private Transform turnLaneCenter;
    private Transform turnLaneRight;
    private float baseForwardSpeed;

    public float coinBank;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {  

        //added

        controller.enabled = false;

         transform.position = new Vector3(1202.7f, 0.9f, 1201f);

        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        currentLaneCenter = transform.position;
        currentLaneCenter.y = transform.position.y;

        verticalVelocity = groundedStickForce;

        baseForwardSpeed = forwardSpeed;

        controller.enabled = true;

        if (animator != null)
        {
            animator.Play("Running");
        }
    }

    private void Update()
    {
        HandleLaneInput();
        HandleTurnInput();
        HandleJumpInput();
        HandleSlideInput();
        MovePlayer();
        UpdateAnimation();

        if(transform.position.y > 3.6f) { haveJumped = true; }
        
        if(haveJumped == true && transform.position.y < 1.4f)
        {
           RestartRunningAnimation();
           haveJumped = false; 
        }

        if (forwardSpeed > 12f)
          {
         ReduceCoinSpeedBoost();
          }
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
        if (Input.GetKeyDown(KeyCode.A) && (canTurnLeft || allowManualTurnDebug))
            TurnLeft();

        if (Input.GetKeyDown(KeyCode.D) && (canTurnRight || allowManualTurnDebug))
            TurnRight();
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !isSliding)
        {
            // Was 2f
            verticalVelocity = Mathf.Sqrt(jumpForce * 2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    private void HandleSlideInput()
    {
        if (Input.GetKeyDown(KeyCode.S) && controller.isGrounded && !isSliding)
        {
            StartCoroutine(SlideRoutine());

            if (animator != null)
            {
                animator.SetTrigger("Slide");
            }
        }
    }

    private IEnumerator SlideRoutine()
    {
        isSliding = true;

        float originalHeight = controller.height;
        Vector3 originalCenter = controller.center;

        controller.height = originalHeight / 2f;
        controller.center = originalCenter + Vector3.down * (originalHeight / 4f);

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        controller.center = originalCenter;

        isSliding = false;
    }

    private void MovePlayer()
    {
        currentLaneCenter += forwardDirection * forwardSpeed * Time.deltaTime;

        Vector3 targetPosition = currentLaneCenter + rightDirection * (currentLane * laneOffset);

        Vector3 currentPosition = transform.position;
        Vector3 flatCurrent = new Vector3(currentPosition.x, 0f, currentPosition.z);
        Vector3 flatTarget = new Vector3(targetPosition.x, 0f, targetPosition.z);

        Vector3 horizontalMove = (flatTarget - flatCurrent) * laneChangeSpeed;

        if (controller.isGrounded && verticalVelocity < 0f)
        {

            verticalVelocity = groundedStickForce;
        }
        else
        {
            if (verticalVelocity > 0f)
                verticalVelocity -= gravity * Time.deltaTime;
            
            else  
                verticalVelocity -= gravity * fallMultiplier * Time.deltaTime;

            verticalVelocity = Mathf.Max(verticalVelocity, maxFallSpeed);

if(transform.position.y > maxJump )
{
           verticalVelocity -= 0.11f;
           maxJump = 1.5f;
}
        }

        if(transform.position.y < 1.3f)
        {
            maxJump = 3.8f;
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

        SnapToTurnLane();
        canTurnLeft = false;
        canTurnRight = false;
    }

    private void TurnRight()
    {
        transform.Rotate(0f, 90f, 0f);

        forwardDirection = transform.forward.normalized;
        rightDirection = transform.right.normalized;

        currentLaneCenter = transform.position - rightDirection * (currentLane * laneOffset);

        SnapToTurnLane();
        canTurnLeft = false;
        canTurnRight = false;
    }

    private void SnapToTurnLane()
    {
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
                closestLaneIndex = i - 1; // 0,1,2 -> -1,0,1
            }
        }

        Vector3 snappedPosition = closestLane.position;
        snappedPosition.y = transform.position.y;
        transform.position = snappedPosition;

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

    public void AddSpeed(float amount)
    {
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed = Mathf.Min(forwardSpeed + amount, maxSpeed);
        }
        else
        {
            coinScoreMultiplier = Mathf.Min(coinScoreMultiplier + 1, maxCoinScoreMultiplier);
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        // Use whichever parameters actually exist in your Animator
        animator.SetFloat("Speed", forwardSpeed);
        animator.SetBool("Grounded", controller.isGrounded);
        animator.SetBool("Sliding", isSliding);
        animator.SetFloat("VerticalVelocity", verticalVelocity);
    }
    private void HandleManualTestTurnInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(0f, -90f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.Rotate(0f, 90f, 0f);
        }
    }

   public void AddToCoinBank()
    {
        coinsCollected++;

        coinScoreMultiplier = Mathf.Min(
            1 + (coinsCollected / coinsPerMultiplierStep),
            maxCoinScoreMultiplier
        );
    }

    public int GetCoinScoreMultiplier()
    {
        return coinScoreMultiplier;
    }

    public int GetCoinScoreValue(int baseCoinValue)
    {
        return baseCoinValue * coinScoreMultiplier;
    }

    public void ResetSpeedAndMultiplier()
    {
        forwardSpeed = baseForwardSpeed;
        coinScoreMultiplier = 1;
        coinsCollected = 0;
    }

    public void RestartRunningAnimation()
    {
         if (animator != null)
        {
            animator.Play("Running");
        }
    }

    public int GetGameLevel()
    {
        
        return TrackGenerator.Instance.GetGameLevel();
    }

    public void ReduceCoinSpeedBoost()
    {

        float speedLoss = (coinBank * Time.deltaTime)/40;
        
            forwardSpeed -= speedLoss;

            forwardSpeed = Mathf.Max(12f, forwardSpeed);

            coinBank -= speedLoss;

            if(forwardSpeed < 14f)
        {
            coinBank = 0;
        }
          
    }
    
}