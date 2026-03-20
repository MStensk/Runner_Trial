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
    public int commonId;
    bool removeBear = false;

public void SetId(int id)
    {
        commonId = id;
    }
    public void SetMoveDirection(string direction)
    {
        moveDirection = direction;
    }

    public void SetRemoveBear()
    {
        removeBear = true;
    }
    private void Start()
    {
        isActiveInnPool = false; 
        startPos = transform.position;
        removeBear = false;
        SetInitialRotation();
    }

    private void Update()
    {
        
        float halfLane = laneLength / 2f;
    
    if(removeBear) return;
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

    public void DeactivateBear()
    {
    removeBear = false;
    moveTimer = 0f;
    isActiveInnPool = true;

     startPos = Vector3.zero;
    moveDirection = "North";
    speed = 4f;

    Transform bearVisual = transform.GetChild(0).GetChild(0).GetChild(0);
    bearVisual.localScale = Vector3.one * 8f;

    transform.position = Vector3.zero;

    transform.rotation = Quaternion.Euler(0, 0, 0);

     SetId(0);
    
    gameObject.SetActive(false);
    

    }
public void ResetBearState()
{
    removeBear = false;
    moveTimer = 0f;
}
public void SetStartPosition(Vector3 newStartPos)
{
    startPos = newStartPos;
}

public void SetBearSize(float grow)
    { 
         Transform bearVisual = transform.GetChild(0).GetChild(0).GetChild(0);

         bearVisual.localScale = new Vector3(grow ,grow, grow);
    
      BoxCollider col = GetComponent<BoxCollider>();

    Vector3 newSize = col.size;
    newSize.z += 0.2f;
    col.size = newSize;
      
}

public void SetBearSpeed(float newSpeed)
{
    speed = newSpeed;
}

/* private void Initialize(Vector3 spawnPosition, string direction)
    {
        transform.position = spawnPosition;
        startPos = spawnPosition;

        moveTimer = 0f;
        isActiveInnPool = true;
        track = FindObjectOfType<TrackGenerator>();

        SetInitialRotation();
    }
*/
}
