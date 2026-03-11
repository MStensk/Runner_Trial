using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;

    public float laneDistance = 6f;   // Distance between lanes
    public float laneChangeSpeed = 10f;

    private int currentLane = 0;      // -1 = left, 0 = middle, 1 = right

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detect input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > -1)
                currentLane-=1;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 1)
                currentLane+=1;
        }
    }

    void FixedUpdate()
    {
        // Forward movement
        Vector3 velocity = rb.linearVelocity;
        velocity.z = forwardSpeed;
        rb.linearVelocity = velocity;

        // Target X position
        Vector3 targetPosition = transform.position;
        targetPosition.x = currentLane * laneDistance;

        // Smooth movement to lane
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }
}