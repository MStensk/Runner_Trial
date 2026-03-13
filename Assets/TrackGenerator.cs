using UnityEngine;
using System.Collections.Generic;

public class TrackGenerator : MonoBehaviour
{
    private Vector3 currentBuildPosition = new Vector3(292.5f, 1.1f, 290f );
    public int initialTrackLength = 1;
    public int turnController = 0;

    public List<string> buildDirections = new List<string>{
    "North", "East", "South","West"};

    public string currentDirection;

    public int directionValue;
    public void InitializeTrack()
    {
        for(int i = 0; i < initialTrackLength; i++)
        {
        GameObject trackPiece = ObstracleCourseLevelOnePool.SharedInstance.GetTrack();
// Mangler skelne mellem om det er et elemnent som skal generere bane ved spiller kontakt, eller ære pasiv
        if(trackPiece != null)
        {

  ObstracleCourseLevelOneController controller = trackPiece.GetComponent<ObstracleCourseLevelOneController>();

 float xDisplacement = controller.lengthOnX;
 float zDisplacement = controller.lengthOnZ;

 float xPosition = xDisplacement / 2;
 float zPosition = zDisplacement / 2;
 //float startXPosition = 192.5;
 //float startZPosition = 200;

            trackPiece.transform.position = new Vector3(currentBuildPosition.x + xPosition, 1.1f, currentBuildPosition.z + zPosition);
            //trackPiece.transform.position = new Vector3(xPosition, 0.1f, zDisplacement);
            
            currentBuildPosition = new Vector3(currentBuildPosition.x, 0.1f, currentBuildPosition.z);
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

         //   BoxCollider collider = trackPiece.GetComponent<BoxCollider>();

      //      collider.center = new Vector3(xPosition, 0.1f, zPosition + 5);

            currentBuildPosition += new Vector3(0.0f, 0.0f, zDisplacement);

            turnController += 1;

        }
    }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directionValue = 0;
currentDirection = buildDirections[directionValue];

        InitializeTrack();
    }

    public void BuildTrack()
    {
    //    Debug.Log("turnController value: " + turnController );
        
        // skal evt tilbage på: turnController >= 4
        if(turnController >= 2)
        {
           int turnDirection = UnityEngine.Random.Range(0, 2);
           Debug.Log("Randum number: " + turnDirection);
            turnController = 0;

     turnDirection = 0;

            if(turnDirection == 0)
            {
                BuildLeftCorner();
            } 
            if(turnDirection == 1)
            {
                BuildRigthCorner();
            }       
            }
        else{
            BuildStraightTrack();
            }
    }
        public void BuildLeftCorner()
    {
       Debug.Log("Build a left corner "); 
        Debug.Log("Build direction: " + currentDirection); 
    GameObject trackPiece = LeftTurnPool.SharedInstance.GetTrack();

LeftTurnController controller = trackPiece.GetComponent<LeftTurnController>();

 float xDisplacement = controller.lengthOnX;
 float zDisplacement = controller.lengthOnZ;

 float xPosition = xDisplacement / 2;
 float zPosition = zDisplacement / 2;

 float newXValue;
 float newZValue;

      if(trackPiece != null)
        {
            if(currentDirection == "North")
            {
             newXValue = currentBuildPosition.x + xPosition;
             newZValue = currentBuildPosition.z + zPosition;

               currentBuildPosition += new Vector3(-21.45f, 0f, +24.5f);

            }
            else if(currentDirection == "East")
            {
             newXValue = currentBuildPosition.x + zPosition;
             newZValue = currentBuildPosition.z - xPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, 90, 0);

                currentBuildPosition += new Vector3(24.8f, 0f, +20.3f);
          
            }
              else if(currentDirection == "South")
            {
             newXValue = currentBuildPosition.x - xPosition;
             newZValue = currentBuildPosition.z - zPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, 180, 0);
           
            currentBuildPosition += new Vector3(21.2f, 0f, -24.8f);
           }
                     else if(currentDirection == "West")
            {
                
             newXValue = currentBuildPosition.x - zPosition;
             newZValue = currentBuildPosition.z + xPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, -90, 0);

            currentBuildPosition += new Vector3(-24.8f, 0f, -21.2f);    
            }
            else{return;}

            trackPiece.transform.position = new Vector3(newXValue, 1.0f, newZValue);
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

          //  currentBuildPosition += new Vector3(0.0f, 0f, zDisplacement);

            directionValue += -1;

            if(directionValue < 0)
            {
                directionValue = 3;
            }

            if(directionValue > 3)
            {
                directionValue = 0;
            }

             SetCurrentDirection();

      //      Debug.Log("newXValue: " + newXValue + " newYValue: " + newYValue);

            turnController = 0;
        }   
    }

  public void BuildRigthCorner()
    {
       Debug.Log("Build a Rigth corner "); 
        Debug.Log("Build direction: " + currentDirection); 
    GameObject trackPiece = RigthTurnPool.SharedInstance.GetTrack();

RigthTurnController controller = trackPiece.GetComponent<RigthTurnController>();

 float xDisplacement = controller.lengthOnX;
 float zDisplacement = controller.lengthOnZ;

 float xPosition = xDisplacement / 2;
 float zPosition = zDisplacement / 2;

 float newXValue;
 float newZValue;

      if(trackPiece != null)
    {
            if(currentDirection == "North")
            {
             newXValue = currentBuildPosition.x + xPosition;
             newZValue = currentBuildPosition.z + zPosition;

               currentBuildPosition += new Vector3(25.45f, 0f, 41.5f);

            }
            else if(currentDirection == "East")
            {
             newXValue = currentBuildPosition.x + zPosition;
             newZValue = currentBuildPosition.z - xPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, 90, 0);

             currentBuildPosition += new Vector3(41.6f, 0f, -41.4f);
            }
              else if(currentDirection == "South")
            {
             newXValue = currentBuildPosition.x - xPosition;
             newZValue = currentBuildPosition.z - zPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, 180, 0);

 currentBuildPosition += new Vector3(-40f, 0f, -41.5f);
 //currentBuildPosition += new Vector3(50f, 0f, -151.5f);
           //   currentBuildPosition += new Vector3(-21.45f, 0f, -34.5f);
            }
            else if(currentDirection == "West")
            {
                
                   newXValue = currentBuildPosition.x - zPosition;
             newZValue = currentBuildPosition.z + xPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, -90, 0);

 currentBuildPosition += new Vector3(-41.2f, 0f, 25.8f);
          //   currentBuildPosition += new Vector3(-25f, 0f, 17.45f);
                
            }
            else{return;}

            trackPiece.transform.position = new Vector3(newXValue, 1.0f, newZValue);
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

          //  currentBuildPosition += new Vector3(0.0f, 0f, zDisplacement);

            directionValue += 1;

            if(directionValue < 0)
            {
                directionValue = 3;
            }

            if(directionValue > 3)
            {
                directionValue = 0;
            }

             SetCurrentDirection();

      //      Debug.Log("newXValue: " + newXValue + " newYValue: " + newYValue);

            turnController = 0;
        }   
    }
    public void BuildStraightTrack()
    {

Debug.Log("Build a Straight track"); 
Debug.Log("Build direction: x " + currentDirection); 
Debug.Log("Byg z position: " + currentBuildPosition.z );

GameObject trackPiece = ObstracleCourseLevelOnePool.SharedInstance.GetTrack();

 ObstracleCourseLevelOneController controller = trackPiece.GetComponent< ObstracleCourseLevelOneController>();

 float xDisplacement = controller.lengthOnX;
 float zDisplacement = controller.lengthOnZ;

 float xPosition = xDisplacement / 2;
 float zPosition = zDisplacement / 2;

 float newXValue;
 float newZValue;


         if(trackPiece != null)
        {
if(currentDirection == "North")
            {
                 newXValue = currentBuildPosition.x + xPosition;
                 newZValue = currentBuildPosition.z + zPosition;

                currentBuildPosition += new Vector3(0.0f, 0f, zDisplacement);

            }
            else if(currentDirection == "East")
            {
                trackPiece.transform.rotation = Quaternion.Euler(0, 90, 0);

                newXValue = currentBuildPosition.x + zPosition;
                newZValue = currentBuildPosition.z - xPosition;

                currentBuildPosition += new Vector3(zDisplacement, 0f, 0.0f);

            }
            else if(currentDirection == "South")
            {
                 trackPiece.transform.rotation = Quaternion.Euler(0, 180, 0);

                 newXValue = currentBuildPosition.x - xPosition;
                 newZValue = currentBuildPosition.z - zPosition;

                currentBuildPosition += new Vector3(0.0f, 0f, - zDisplacement);
            }
            else if(currentDirection == "West")
            {
                // West
                 trackPiece.transform.rotation = Quaternion.Euler(0, -90, 0);

                newXValue = currentBuildPosition.x - zPosition;
                newZValue = currentBuildPosition.z + xPosition;

                currentBuildPosition += new Vector3(-zDisplacement, 0f, 0.0f);
            }
            else{return;}

          //  newXValue = currentBuildPosition.x + xPosition;
           // newYValue = currentBuildPosition.z + zPosition;
            Debug.Log("Placement nexZValue" + newZValue );
            trackPiece.transform.position = new Vector3(newXValue, 1.1f, newZValue);
            
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

       //     currentBuildPosition += new Vector3(0.0f, 0f, zDisplacement);
       
       turnController += 1;
        }   
    }

    public void SetCurrentDirection()
    {
        currentDirection = buildDirections[directionValue];
    }

    // Update is called once per frame
    void Update()
    {
       
    }  
}
