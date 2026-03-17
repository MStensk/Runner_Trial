using UnityEngine;
using System.Collections.Generic;
using System;
using System.Data;
using Unity.Collections;
using System.Linq;
using NUnit.Framework;

public class TrackGenerator : MonoBehaviour
{
    private Vector3 currentBuildPosition = new Vector3(292.5f, 1.1f, 290f );
    public int initialTrackLength = 1;
    public int turnController = 0;

    public List<string> buildDirections = new List<string>{
    "North", "East", "South","West"};

    public string currentDirection;
    public int directionValue;
    int counter = 0; 
float coinDistance = 2.0f;
    
    public void InitializeTrack()
    {
   Debug.Log("InitialTrackLength; " + initialTrackLength);

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

            trackPiece.transform.position = new Vector3(currentBuildPosition.x + xPosition, 1.1f, currentBuildPosition.z + zPosition);
 
            currentBuildPosition = new Vector3(currentBuildPosition.x, 0.1f, currentBuildPosition.z);
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

            currentBuildPosition += new Vector3(0.0f, 0.0f, zDisplacement);

            turnController += 1;

            counter += 1;

Debug.Log("antal track bygget initialize: " + counter); 
            

        }
        else{ Debug.Log("No straigth tracks in pool"); }
    }
  //   BuildTrack();
   //  BuildTrack();
  //   BuildTrack();
  

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
          Debug.Log("turncontroller value: " + turnController);
        if(turnController >= 4)
        {
           int turnDirection = UnityEngine.Random.Range(0, 2);
        //   Debug.Log("Randum number: " + turnDirection);

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

            directionValue += -1;

            if(directionValue < 0)  directionValue = 3; 

             SetCurrentDirection();

            turnController = 0;
        }   
    }

  public void BuildRigthCorner()
    {
    //   Debug.Log("Build a Rigth corner "); 
     //   Debug.Log("Build direction: " + currentDirection); 
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

            }
            else if(currentDirection == "West")
            {
                
             newXValue = currentBuildPosition.x - zPosition;
             newZValue = currentBuildPosition.z + xPosition;

             trackPiece.transform.rotation = Quaternion.Euler(0, -90, 0);

             currentBuildPosition += new Vector3(-41.2f, 0f, 25.8f);
        
                
            }
            else{return;}

            trackPiece.transform.position = new Vector3(newXValue, 1.0f, newZValue);
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

            directionValue += 1;

            if(directionValue > 3) directionValue = 0;
            
             SetCurrentDirection();

            turnController = 0;
        }   
    }
    public void BuildStraightTrack()
    {

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
             
                trackPiece.transform.rotation = Quaternion.Euler(0, -90, 0);

                newXValue = currentBuildPosition.x - zPosition;
                newZValue = currentBuildPosition.z + xPosition;

                currentBuildPosition += new Vector3(-zDisplacement, 0f, 0.0f);
            }
            else{return;}

            trackPiece.transform.position = new Vector3(newXValue, 1.1f, newZValue);
            
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

            turnController += 1;

            PlaceObstraclesStraigthTrack(trackPiece, currentDirection);     
        }   
    }

public void PlaceObstraclesStraigthTrack(GameObject trackPiece, String currentDirection)
    {
     //   Debug.Log("PlaceObstraclesStraigthTrack have been called");
        
        float xBase = trackPiece.transform.position.x;
     //   Debug.Log("xBase value: " + xBase);
        float zBase = trackPiece.transform.position.z;
      //  Debug.Log("zBase value: " + zBase);
       
        float yBase = trackPiece.transform.position.y;
        float sectionOneX;
        float sectionTwoX;
        float sectionThreeX;
        float sectionFourX;
        float sectionFiveX;
        float sectionSixX;
        float sectionOneZ;
        float sectionTwoZ;
        float sectionThreeZ;
        float sectionFourZ;
        float sectionFiveZ;
        float sectionSixZ;

        float selectedX;
        float selectedZ;
       
        float reverse =-11.7f;
        float leftPart = -4.5f;
        float rigthPart = 4.5f;

        int coinSpawn = 3;
        int mudSpawn = 0;
        int speedSpawn = 0;
        int bearSpawn = 2;

        int isBearSpawn = 0;
        int bearSpawnLaneFinder = 0;
        int usedLaneOne = 0;
        int usedLaneTwo = 0;

        int spawnNumber = 1;

       List<string> frontSections= new List<string>{
    "sectorFour", "sectorFive", "sectorSix"};

     List<string> backSections = new List<string>{
    "setorOne", "sectorTwo", "sectorThree"};

        if(currentDirection == "North")
        {
         //   Debug.Log("North direction found");
            
         sectionOneX = xBase + leftPart;
         sectionTwoX = xBase;
         sectionThreeX = xBase + rigthPart;
         sectionFourX = xBase + leftPart;
         sectionFiveX = xBase;
         sectionSixX = xBase + rigthPart;  
         sectionOneZ = zBase + reverse;
         sectionTwoZ = zBase + reverse;
         sectionThreeZ = zBase + reverse;
         sectionFourZ =zBase + 0.7f;
         sectionFiveZ =zBase + 0.7f;
         sectionSixZ =zBase + 0.7f;       
        }
        else if(currentDirection == "East")
        {
        //    Debug.Log("East direction found");
           
         sectionOneX = xBase + reverse;
         sectionTwoX = xBase + reverse;
         sectionThreeX = xBase + reverse;
         sectionFourX = xBase + 0.7f;
         sectionFiveX = xBase + 0.7f;
         sectionSixX = xBase + 0.7f;  
         sectionOneZ = zBase + leftPart;
         sectionTwoZ = zBase;
         sectionThreeZ = zBase + rigthPart;
         sectionFourZ = zBase + leftPart;
         sectionFiveZ = zBase;
         sectionSixZ = zBase + rigthPart;     
        }
            else if(currentDirection == "South")
            {
          
         sectionOneX = xBase - leftPart;
         sectionTwoX = xBase;
         sectionThreeX = xBase - rigthPart;
         sectionFourX = xBase - leftPart;
         sectionFiveX = xBase;
         sectionSixX = xBase - rigthPart;  
         sectionOneZ = zBase - reverse;
         sectionTwoZ = zBase - reverse;
         sectionThreeZ = zBase - reverse;
         sectionFourZ =zBase - 0.7f;
         sectionFiveZ =zBase - 0.7f;
         sectionSixZ =zBase - 0.7f;       
            }
            else  //if(currentDirection == "West")
        {
          //West
       //   Debug.Log("West direction found");
          
         sectionOneX = xBase - reverse;
         sectionTwoX = xBase - reverse;
         sectionThreeX = xBase - reverse;
         sectionFourX = xBase - 0.7f;
         sectionFiveX = xBase - 0.7f;
         sectionSixX = xBase - 0.7f;  
         sectionOneZ = zBase - leftPart;
         sectionTwoZ = zBase;
         sectionThreeZ = zBase - rigthPart;
         sectionFourZ = zBase - leftPart;
         sectionFiveZ = zBase;
         sectionSixZ = zBase - rigthPart;       
        }    

Dictionary<string, int> spawnHierarchy = new Dictionary<string, int>()
{
    {"coinSpawnValue", coinSpawn},
    { "bearSpawnValue", bearSpawn},
    {  "mudSpawnValue", mudSpawn},
    { "speedSpawnValue", speedSpawn}
     };

       var highest = spawnHierarchy.OrderByDescending(x => x.Value)
       .Take(2)
       .ToList();

Debug.Log("Var highest: Aktive trap build navn " + highest);

for(int i = 0; i < 2; i++)
        {
            String currentSpawn = highest[i].Key;

            Debug.Log("currentSpawn: " + currentSpawn);
            Debug.Log("spawnNumber: " + spawnNumber);
 
 int laneNumber = UnityEngine.Random.Range(0, 3);
Debug.Log("laneNumber value: " + laneNumber);
 
 if(spawnNumber == 1)
            {
if(laneNumber == 1)
                {
                selectedX = sectionOneX;
                selectedZ = sectionOneZ;
                usedLaneOne = 1;
                 }
                else if(laneNumber == 2)
                {
                selectedX = sectionTwoX;
                selectedZ = sectionTwoZ;
                usedLaneOne = 2;
              
                }
                else
                {
                selectedX = sectionThreeX;
                selectedZ = sectionThreeZ;
                usedLaneOne = 3;
                }
                 spawnNumber += 1;
// Makes sure that it is known what lane number BearMovement is placed in
                bearSpawnLaneFinder = 1;

                Debug.Log("first section"); 
                } 
                // Skal måske ændres til else, så alle udfale er i betragtning
            else if(spawnNumber == 2)
            {
                 Debug.Log("last section");
                if(laneNumber == 1)
               {
                selectedX = sectionFourX;
                selectedZ = sectionFourZ;
                usedLaneTwo = 1;
               }
               else if(laneNumber == 2)
                {
                selectedX = sectionFiveX;
                selectedZ = sectionFiveZ;
                usedLaneTwo = 2;
                
                } 
                else
                {
                selectedX = sectionSixX;
                selectedZ = sectionSixZ;
                usedLaneTwo = 3;
               
                }
                 spawnNumber += 1; 
// Makes sure that it is known what lane number BearMovement is placed in
                 bearSpawnLaneFinder = 2;
       // When usedLaneOne  is set to 4, we knaw that all other lanes in row is blocked

            }
            else if(spawnNumber == 3)
            {
                if(bearSpawnLaneFinder == 1)
                {
                     usedLaneOne = 4;
                }
                else if(bearSpawnLaneFinder == 2)
                {
                    usedLaneTwo = 4;
                }

int laneNumberThirdSpawn = UnityEngine.Random.Range(0, 2);
    
                if(usedLaneOne == 4)
                {
                  if(usedLaneTwo == 1)
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionFiveX;
                             selectedZ = sectionFiveZ;
                        }
                        else
                        {
                            selectedX = sectionSixX;
                            selectedZ = sectionSixZ;
                        }
                    }  
                    else if (usedLaneTwo == 2)
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionFourX;
                             selectedZ = sectionFourZ;
                        }
                        else
                        {
                            selectedX = sectionSixX;
                            selectedZ = sectionSixZ;
                        }
                    } 
                    else
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionFourX;
                             selectedZ = sectionFourZ;
                        }
                        else
                        {
                            selectedX = sectionFiveX;
                            selectedZ = sectionFiveZ;
                        }
                    } 
                }
                else if(usedLaneTwo == 4)
                {
                  if(usedLaneOne == 1)
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionTwoX;
                             selectedZ = sectionTwoZ;
                        }
                        else
                        {
                            selectedX = sectionThreeX;
                            selectedZ = sectionThreeZ;
                        }
                    }  
                    else if (usedLaneOne == 2)
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionOneX;
                             selectedZ = sectionOneZ;
                        }
                        else
                        {
                            selectedX = sectionThreeX;
                            selectedZ = sectionThreeZ;
                        }
                    } 
                    else
                    {
                        if(laneNumberThirdSpawn == 1)
                        {
                             selectedX = sectionOneX;
                             selectedZ = sectionOneZ;
                        }
                        else
                        {
                            selectedX = sectionTwoX;
                            selectedZ = sectionTwoZ;
                        }
                    } 
                }
                else
                {
                    //SKAL FJERNES
                      selectedX = sectionTwoX;
                            selectedZ = sectionTwoZ;
                }
            }  // Skal fjernes
             else{  selectedX = sectionTwoX;
                            selectedZ = sectionTwoZ;}
          
  if(currentSpawn == "coinSpawnValue")
            {
         for(float j = 0.0f; j < 11.7f; j += 2.0f)
                {
 GameObject coin = CoinPool.SharedInstance.GetTrack();
 CoinController controller = coin.GetComponent<CoinController>();        
                      
                      if(currentDirection == "North")
                    {
            //             Debug.Log("North coin placed");
                         coin.transform.position = new Vector3(selectedX, 1.1f, selectedZ + j);
               //         Debug.Log("North coin placed, selectedX: " + selectedX);
              //          Debug.Log("North coin placed, selectedZ: " + selectedZ);
                    }
                         
                      else if(currentDirection == "East")
                    {
                 //       Debug.Log("East coin placed");
                         coin.transform.position = new Vector3(selectedX + j, 1.1f, selectedZ);
                         coin.transform.rotation = Quaternion.Euler(0, 90, 0);

                    }
                         
                     else if(currentDirection == "South")
                    {
                //        Debug.Log("South coin placed");
                         coin.transform.position = new Vector3(selectedX, 1.1f, selectedZ - j);
                         coin.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                     else
                    {
                        //West
                //        Debug.Log("West coin placed");
                         coin.transform.position = new Vector3(selectedX - j, 1.1f, selectedZ);
                         coin.transform.rotation = Quaternion.Euler(0, -90, 0);

                  //         Debug.Log("West coin placed, selectedX: " + selectedX);
                     //       Debug.Log("West coin placed, selectedZ: " + selectedZ);
                    }
                     
            coin.SetActive(true);
               controller.isActiveInnPool = false;
                }
     
            }
            else if(currentSpawn == "speedSpawnValue")
            {
                //Do something
                 Debug.Log("currentSpawn == speedSpawnValue");
            }
            else if(currentSpawn == "bearSpawnValue")
            {
    GameObject bearMovement = BearPool.SharedInstance.GetTrack();
    BearMovement controller = bearMovement.GetComponent<BearMovement>(); 

controller.SetMoveDirection(currentDirection);

// selectedX and Z is used different and lane number is ignored for Bears, 
//because they have to begin in left lane. 
// spawnNumber is 2 for first row and others for last row, because 1 is already added to the value. 

    if(spawnNumber == 2)
                {
                     bearMovement.transform.position = new Vector3(sectionTwoX, 1.1f, sectionTwoZ);
                } 
                else
                {
                   bearMovement.transform.position = new Vector3(sectionFiveX, 1.1f, sectionFiveZ);  
                }

Debug.Log("Bear have been placed: ");
 Debug.Log("Bear place CurrentDirection: " + currentDirection);
 
    bearMovement.SetActive(true);
    controller.isActiveInnPool = false;
// Aktivates that we know a Bear have been placed, so no other object is placed in that lane.
// And set it to the correct lane
  //  isBearSpawn = bearSpawnLaneFinder;
            if(bearSpawnLaneFinder == 1)
                {
                    
                    
                    
                }
         }
            else
            {
                Debug.Log("currentSpawn == else");
                // do something, men bliver nok uden else
            }
         
        }

spawnNumber = 1;
usedLaneOne = 0;
usedLaneTwo = 0;
 //isBearSpawn = 0;
 //bearSpawnLaneFinder = 0;

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
