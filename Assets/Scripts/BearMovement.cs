using UnityEngine;

public class BearMovement : MonoBehaviour
{
public bool isActiveInnPool;

 TrackGenerator track; 

    [SerializeField] private float laneLength = 12f;   // længden den kan bevæge sig
    [SerializeField] private float speed = 4f;   
         // move speed

    private Vector3 startPos;
    private string moveDirection = "North";
    private float moveTimer;
    private int updateCount = 1;

    public int commonID;

public void SetId(int id)
    {
        commonID = id;
    }
    public void SetMoveDirection(string direction)
    {
        moveDirection = direction;
    }

    private void Initialize(Vector3 spawnPosition, string direction)
    {
        transform.position = spawnPosition;
        startPos = spawnPosition;

        moveTimer = 0f;
        isActiveInnPool = true;
        track = FindObjectOfType<TrackGenerator>();

        SetInitialRotation();
    }
    private void Start()
    {
        isActiveInnPool = false; 
        startPos = transform.position;
        SetInitialRotation();
    }

    private void Update()
    {
        float halfLane = laneLength / 2f;
    
        // bevæger sig mellem -halfLane og +halfLane relativt til start pos
        moveTimer += Time.deltaTime;
        float xOffset = Mathf.PingPong(moveTimer * speed, laneLength) - halfLane;

    if(moveDirection == "North" || moveDirection == "South")
        {
            transform.position = new Vector3(
                startPos.x + xOffset,
                startPos.y,
                startPos.z
            );
        }
            else
            {
                transform.position = new Vector3(
                startPos.x,
                startPos.y,
                startPos.z + xOffset
                );
            }

        // Detect direction skift, gør brug af float da movement sjældent lander på præcise tal
        if (xOffset >= halfLane - 0.01f)
            {
                FaceLeft();
            }

            if (xOffset <= -halfLane + 0.01f)
            {
                FaceRight();
            }
    }

    private void SetInitialRotation()
    {
        FaceRight();
    }

    private void FaceRight()
    {
        if (moveDirection == "North" || moveDirection == "South")
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void FaceLeft()
    {
        if (moveDirection == "North" || moveDirection == "South")
            transform.rotation = Quaternion.Euler(0, -90, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
