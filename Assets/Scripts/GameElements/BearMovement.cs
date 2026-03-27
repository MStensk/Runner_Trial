using UnityEngine;

public class BearMovement : MonoBehaviour
{

    BearWalkController bearWalkController;
public bool isActiveInnPool;

    [SerializeField] private float laneLength = 12f;   // længden den kan bevæge sig
    [SerializeField] private float speed = 4f;   
         // move speed
    private Vector3 startPos;

    //Calibrate bear y posisition
    private Vector3 bearY = new Vector3 (0f, 0.0f, 0f);
    private string moveDirection = "North";
    private float moveTimer;
    public int commonId;
    bool removeBear = false;
    bool bearWait = true;

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

        bearWalkController = GetComponentInChildren<BearWalkController>();
       
        SetInitialRotation();
    }

    private void Update()
    {
        
        float halfLane = laneLength / 2f;
    
    if(removeBear) return;
        // bevæger sig mellem -halfLane og +halfLane relativt til start pos
        if(bearWait) return;
        
        moveTimer += Time.deltaTime;
        float xOffset = Mathf.PingPong(moveTimer * speed, laneLength) - halfLane;

    if(moveDirection == "North" || moveDirection == "South")
        {
          transform.position = new Vector3(
                startPos.x + xOffset,
                transform.position.y,
                startPos.z
            );
        } // bearY.y,
            else
            {
                transform.position = new Vector3(
                startPos.x,
               transform.position.y,
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
    speed = 4f;
    bearWait = true;
    startPos = Vector3.zero;
    moveDirection = "North";
   
Transform bearVisual = transform.GetChild(0).GetChild(0).GetChild(0);
    bearVisual.localScale = Vector3.one * 8f;

BoxCollider col = GetComponent<BoxCollider>();
    Vector3 newSize = col.size;
newSize.y = 2;
col.size = newSize;

    transform.position = Vector3.zero;

    transform.rotation = Quaternion.Euler(0, 0, 0);

     SetId(0);
 
bearWalkController.ResetCollider();

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

/* gl version
public void SetBearSize(float grow, float collider)
    { 
         Transform bearVisual = transform.GetChild(0).GetChild(0).GetChild(0);

         bearVisual.localScale = new Vector3(grow ,grow, grow);
    
      BoxCollider col = GetComponent<BoxCollider>();

    Vector3 newSize = col.size;
    newSize.y = collider;
    col.size = newSize;
      
}
*/

public void SetBearSize(float grow, float bodyColliderHeight, float damageColliderSize)
{
    Transform bearVisual = transform.GetChild(0).GetChild(0).GetChild(0);
    bearVisual.localScale = new Vector3(grow, grow, grow);

    BoxCollider bodyCol = GetComponent<BoxCollider>();
    if (bodyCol != null)
    {
        Vector3 center = bodyCol.center;
        center.y = bodyColliderHeight;
        bodyCol.center = center;

        Vector3 size = bodyCol.size;
        size.y = bodyColliderHeight / 2f;
        bodyCol.size = size;
    }

    Transform walkColliderTransform = transform.GetChild(0);
    BoxCollider damageCol = walkColliderTransform.GetComponent<BoxCollider>();
    if (damageCol != null)
    {
        Vector3 damageSize = damageCol.size;
        damageSize.x = damageColliderSize/3;
        damageSize.z = damageColliderSize;
        damageCol.size = damageSize;

        Vector3 damageCenter = damageCol.center;
        damageCenter.y = damageColliderSize * 0.5f;
        damageCol.center = damageCenter;
    }
}

public void SetBearSpeed(float newSpeed)
{
    speed = newSpeed;
}

 private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

//Activats bear walk, when character get0s close, physics kan work on bear and ground it. 
 bearWait = false;
        }

public void StartBearWalk()
    {
        bearWait = false;
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
