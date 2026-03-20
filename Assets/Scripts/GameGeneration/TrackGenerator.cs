using UnityEngine;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;

public class TrackGenerator : MonoBehaviour
{
    CoinPool coinPool;
    BearPool bearPool;
    WoodenFirePool woodenFirePool;
    WoodenFencePool woodenFencePool;
   //private Vector3 currentBuildPosition = new Vector3(292.5f, 1.1f, 290f );
        private Vector3 currentBuildPosition = new Vector3(1192.5f, 1.1f, 1190f );
    public int initialTrackLength = 4;
    public int turnController = 0;
    int straigthPartLength = 4;
    int gameLevel = 1;
    int currentCount = 0;
    int neededElements = 10;
    public List<string> buildDirections = new List<string>{
    "North", "East", "South","West"};

    public string currentDirection;
    public int directionValue;

    // Used to change behavior based on what number of possible 
    // insets the current is when placing elements on traCK
    int spawnId = 1;

    // Priority values for elements.
     int coinSpawn = 1000;
     int woodenFenceSpawn = 150;
     int bearSpawn = 300;
     int woodenFireSpawn = 700;
     int coinSpawnAdd = 0;
     int woodenFenceSpawnAdd = 0;
     int bearSpawnAdd = 0;
     int woodenFireSpawnAdd = 0;
     int noSpawn = 0;
    float bearSize = 8;
    float bearSpeed = 4;
  
    public void UpdateLevel()
    {
        gameLevel += 1;
        // Finds new lane formation
        SetStraightPartLength();
        BoostBearSize();
    }

    public void ManageLevelUpdate()
    {
        
        currentCount += 1;

        if(currentCount >= neededElements)
        {
            UpdateLevel();

            currentCount = 0;
        }
    }

    public void SetTrapCommonFactor()
    {
      coinSpawn += 1000 + coinSpawnAdd;
      woodenFenceSpawn += 200 + woodenFenceSpawnAdd;
      bearSpawn += 350 + bearSpawnAdd;
      woodenFireSpawn += 650 + woodenFireSpawnAdd;
    }

// Changes trap distribution over time
public void BuildCommonFactor()
    {
       coinSpawnAdd += 3;
       bearSpawnAdd += 12;
       woodenFireSpawnAdd += 6;
       woodenFenceSpawnAdd += 4;
    }

    public void BoostBearSize()
    {
        if(bearSize > 43.9f) return;
        
       if(gameLevel > 3 && gameLevel < 7)
         {
            bearSize += 3.0f;
        }
        else if(gameLevel >= 18 && gameLevel < 29 )
        {
            bearSize += 0.4f;
        }
        else if(gameLevel >= 29 && gameLevel < 34)
        {
            bearSize += 2.0f;
        }
        else if(gameLevel >= 45 && gameLevel < 66)
        {
            bearSize += 0.5f;
        }
         else if(gameLevel >= 100 && gameLevel < 111)
        {
            bearSize += 0.4f;
        }

    }

    public void BoostBearSpeed()
    {
         if(bearSpeed > 8f)
        {
            bearSpeed = 8f;
        }

        if(gameLevel > 2 && gameLevel < 5)
        {
            bearSpeed += 0.4f;
        }
        else if(gameLevel >= 8 && gameLevel < 11 )
        bearSpeed -= 0.4f;
        else if(gameLevel >= 11 && gameLevel < 18)
        {
             bearSpeed += 0.3f;
        }
        else if(gameLevel >= 18 && gameLevel < 29)
        {
            bearSpeed -= 0.2f;
        }
         else if(gameLevel >= 34 && gameLevel < 45)
        {
            bearSpeed += 0.1f;
        }
        else if(gameLevel >= 45 && gameLevel < 66)
        {
            bearSpeed -= 0.08f;
        }
         else if(gameLevel >= 66 && gameLevel < 200)
        {
            bearSpeed += 0.03f;
        }

    }

public void SetStraightPartLength()
    {
        int randomLaneNumberThirdSpawn;

        int random =  UnityEngine.Random.Range(1, 100);
        
        if(random <= 10)
        {
            randomLaneNumberThirdSpawn = 0;
        }
        else if(random <= 25)
        { 
            randomLaneNumberThirdSpawn = UnityEngine.Random.Range(5, 8);
        }
        else if(random <= 35)
        {
            randomLaneNumberThirdSpawn = 1;
        }
        else if(random <= 40)
        {
            randomLaneNumberThirdSpawn = 2;
        }
        else
        {
            randomLaneNumberThirdSpawn = UnityEngine.Random.Range(3, 5);
        }
        
        straigthPartLength = randomLaneNumberThirdSpawn;
    }

    public void InitializeTrack()
    {

        for(int i = 0; i < initialTrackLength; i++)
        {
        GameObject trackPiece = ObstracleCourseLevelOnePool.SharedInstance.GetTrack();

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

        }
        else{ Debug.Log("No straigth tracks in pool"); }

        BuildTrack();
        BuildTrack();
        BuildTrack();
    
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
// || currentBuildPosition.x < 400|| currentBuildPosition.z > 2000 || currentBuildPosition.z < 400)

int turnDirection = UnityEngine.Random.Range(0, 2);

// Makes sure game doesn´t leave tarrain or get close to edges.
        if(currentBuildPosition.x > 2000)
        {
           
            if(currentDirection == "North")
            {
                  BuildLeftCorner();
                
            }
            else if(currentDirection == "South")
            {
                BuildRigthCorner();
             
            }
            else if(currentDirection == "West")
            {
                BuildStraightTrack();
            }
            else
            {
                  if(turnDirection == 0)
            {
                BuildLeftCorner();
             
            } 
            else
            {
                BuildRigthCorner();
                
            }  
            }
        }
        else if( currentBuildPosition.x < 400)
            {
                  if(currentDirection == "North")
            {
              BuildRigthCorner();    
            }
            else if(currentDirection == "South")
            {  
              BuildLeftCorner();
            }
            else if(currentDirection == "West")
            {
            if(turnDirection == 0)
            {
                BuildLeftCorner(); 
            } 
         else
            {
                BuildRigthCorner();
            }   
          
            }
            }
            else if( currentBuildPosition.z < 400)
            {
               if(currentDirection == "East")  
            {
              BuildLeftCorner();
            }   
            else if(currentDirection == "West")
            {  
               BuildRigthCorner();
            } 
            else if(currentDirection == "South")
            {
            if(turnDirection == 0)
            {
                BuildLeftCorner(); 
            } 
            if(turnDirection == 1)
            {
                BuildRigthCorner();
            }   

            }
            }
            else if( currentBuildPosition.z > 2000)
            {
               if(currentDirection == "East")  
            {
                BuildRigthCorner();    
            }   
            else if(currentDirection == "West")
            {  
               BuildLeftCorner();
            } 
            else if(currentDirection == "North")
            {
            
            if(turnDirection == 0)
            {
                BuildLeftCorner(); 
            } 
           else
            {
                BuildRigthCorner();
            }
            }   
            else{

            if(turnDirection == 0)
            {
                BuildLeftCorner();
             
            } 
            if(turnDirection == 1)
            {
                BuildRigthCorner();
            
            }       
        }
     }
        else{
        if(turnController >= straigthPartLength)
        {
           
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
            BuildCommonFactor();
        }

            ManageLevelUpdate();
            SetTrapCommonFactor();
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
            // skal undersøges om det skal være andet end else (return;)
            else{return;}

            trackPiece.transform.position = new Vector3(newXValue, 1.1f, newZValue);
            
            trackPiece.SetActive(true);
            
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = true;

            // Set to recognice wich objects on track should return to pools
            controller.SetId(spawnId);

            turnController += 1;

            PlaceObstraclesStraigthTrack(trackPiece, currentDirection);     
        }   
    }

public void PlaceObstraclesStraigthTrack(GameObject trackPiece, String currentDirection)
    {
     
        float xBase = trackPiece.transform.position.x;

        float zBase = trackPiece.transform.position.z;
    
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

        float reverse =-5.8f;
        float leftPart = -4.5f;
        float rigthPart = 4.5f;

        int usedRowOne = 0;
        int usedRowTwo = 0;

        int spawnNumber = 1;

       List<string> frontSections= new List<string>{
    "sectorFour", "sectorFive", "sectorSix"};

     List<string> backSections = new List<string>{
    "setorOne", "sectorTwo", "sectorThree"};

        if(currentDirection == "North")
        {
      
         sectionOneX = xBase + leftPart;
         sectionTwoX = xBase;
         sectionThreeX = xBase + rigthPart;
         sectionFourX = xBase + leftPart;
         sectionFiveX = xBase;
         sectionSixX = xBase + rigthPart;  
         sectionOneZ = zBase + reverse;
         sectionTwoZ = zBase + reverse;
         sectionThreeZ = zBase + reverse;
         sectionFourZ =zBase + 5.9f;
         sectionFiveZ =zBase + 5.9f;
         sectionSixZ =zBase + 5.9f;       
        }
        else if(currentDirection == "East")
        {
           
         sectionOneX = xBase + reverse;
         sectionTwoX = xBase + reverse;
         sectionThreeX = xBase + reverse;
         sectionFourX = xBase + 5.9f;
         sectionFiveX = xBase + 5.9f;
         sectionSixX = xBase + 5.9f;  
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
         sectionFourZ =zBase - 5.9f;
         sectionFiveZ =zBase - 5.9f;
         sectionSixZ =zBase - 5.9f;       
            }
            else 
        {
     //West
         sectionOneX = xBase - reverse;
         sectionTwoX = xBase - reverse;
         sectionThreeX = xBase - reverse;
         sectionFourX = xBase - 5.9f;
         sectionFiveX = xBase - 5.9f;
         sectionSixX = xBase - 5.9f;  
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
    {  "woodenFenceSpawnValue", woodenFenceSpawn},
    { "woodenFireSpawnValue", woodenFireSpawn},
    {"noSpawns", noSpawn}
     };

       var highest = spawnHierarchy.OrderByDescending(x => x.Value)
       .Take(4)
       .ToList();

for(int i = 0; i < 4; i++)
        {
            String currentSpawn = highest[i].Key;

 int laneNumber = UnityEngine.Random.Range(1, 4);

 if(spawnNumber == 1)
            {
if(laneNumber == 1)
                {
                selectedX = sectionOneX;
                selectedZ = sectionOneZ;
                usedRowOne = 1;
                 }
                else if(laneNumber == 2)
                {
                selectedX = sectionTwoX;
                selectedZ = sectionTwoZ;
                usedRowOne = 2;
              
                }
                else
                {
                selectedX = sectionThreeX;
                selectedZ = sectionThreeZ;
                usedRowOne = 3;
                }
                 spawnNumber += 1;

                }
            else if(spawnNumber == 2)
            {
     
                if(laneNumber == 1)
               {
                selectedX = sectionFourX;
                selectedZ = sectionFourZ;
                usedRowTwo = 1;
              
               }
               else if(laneNumber == 2)
                {
                selectedX = sectionFiveX;
                selectedZ = sectionFiveZ;
                usedRowTwo = 2;
              }
                else
                {
                selectedX = sectionSixX;
                selectedZ = sectionSixZ;
                usedRowTwo = 3;
               
                }
                 spawnNumber += 1; 

       // When usedLaneOne is set to 4, we know that all other lanes in row is blocked
            } 
            else if(spawnNumber == 3)
     {
        // If third spawn is a bear, its ignored and next object is selected instead. Because Bears
        //only can be placed in empty rows.

            if(currentSpawn == "bearSpawnValue" || currentSpawn == "woodenFenceSpawnValue") { continue; }
               
 // Aktivates that we know a Bear or fence have been placed, so no other object is placed in that lane.
int randomLaneNumberThirdSpawn = UnityEngine.Random.Range(0, 2);

 // Makes sure that nothing else is placed in a row, if there is a bear
                if(usedRowOne == 4 || usedRowOne == 5)
                {
                  if(usedRowTwo == 1)
                    {
                        if(randomLaneNumberThirdSpawn == 1)
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
                    else if (usedRowTwo == 2)
                    {
                        if(randomLaneNumberThirdSpawn == 1)
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
                   
                    if(usedRowTwo == 4 || usedRowTwo == 5) break;

                        if(randomLaneNumberThirdSpawn == 1)
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
                else 
                {

                  if(usedRowOne == 1)
                    {
                        if(randomLaneNumberThirdSpawn == 1)
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
                    else if (usedRowOne == 2)
                    {
                        if(randomLaneNumberThirdSpawn == 1)
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
                        if(randomLaneNumberThirdSpawn == 1)
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
                spawnNumber += 1;
            }
            else{    selectedX = sectionTwoX;
                            selectedZ = sectionTwoZ; break;}
                 
  if(currentSpawn == "coinSpawnValue")
            {
                //Moves placement from mid point in lane to bigin.
float placeFirstCoinAtLaneBeginPoint = 5.9f;

         for(float j = 0.0f; j < 11.7f; j += 2.0f)
                {
 GameObject coin = CoinPool.SharedInstance.GetTrack();
 CoinController controller = coin.GetComponent<CoinController>();  

//Sets identifier for removement 
 controller.SetId(spawnId);      
                      
                      if(currentDirection == "North")
                    {
            
                         coin.transform.position = new Vector3(selectedX, 1.1f, selectedZ + j - placeFirstCoinAtLaneBeginPoint);
           
                    }
                         else if(currentDirection == "East")
                    {
               
                         coin.transform.position = new Vector3(selectedX + j -placeFirstCoinAtLaneBeginPoint, 1.1f, selectedZ);
                         coin.transform.rotation = Quaternion.Euler(0, 90, 0);

                    }
                         else if(currentDirection == "South")
                    {
               
                         coin.transform.position = new Vector3(selectedX, 1.1f, selectedZ - j + placeFirstCoinAtLaneBeginPoint);
                         coin.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                     else
                    {
                        //West
           
                         coin.transform.position = new Vector3(selectedX - j + placeFirstCoinAtLaneBeginPoint, 1.1f, selectedZ);
                         coin.transform.rotation = Quaternion.Euler(0, -90, 0);

                    }
                     
               coin.SetActive(true);
               controller.isActiveInnPool = false;

               // Resets priority for coins.
                coinSpawn = 0;
     
                }
     
            }
            else if(currentSpawn == "woodenFireSpawnValue")
            {

    GameObject woodenFire = WoodenFirePool.SharedInstance.GetTrack();
    WoodenFireController controller = woodenFire.GetComponent<WoodenFireController>(); 

    //Sets identifier for removement 
               controller.SetId(spawnId); 

               woodenFire.transform.position = new Vector3(selectedX, 1.1f, selectedZ);

               woodenFire.SetActive(true);
               controller.isActiveInnPool = false;
// Reset priority
               woodenFireSpawn = 0;

            }
       else if(currentSpawn == "bearSpawnValue")
            {
   
    GameObject bearMovement = BearPool.SharedInstance.GetTrack();
    BearMovement controller = bearMovement.GetComponent<BearMovement>(); 


// spawnNumber == 2, means its first spawn, because of spawnNumber += 1 earlier
// 4 Blocks the row if bear is placed. 
// selectedX and Z is used different and lane number is ignored for Bears, 
//because they have to begin in left lane. 
// spawnNumber is 2 for first row and others for last row, because 1 is already added to the value. 
if(spawnNumber == 2)
                {
                     usedRowOne = 4;
                     bearMovement.transform.position = new Vector3(sectionTwoX, 1.1f, sectionTwoZ);
              
                }
                else
                {
                    usedRowTwo = 4;
                    bearMovement.transform.position = new Vector3(sectionFiveX, 1.1f, sectionFiveZ);
                }



//Sets identifier for removement 
 controller.SetId(spawnId); 

controller.SetMoveDirection(currentDirection);
controller.SetStartPosition(bearMovement.transform.position);
 controller.ResetBearState();
 
controller.SetBearSize(bearSize);

 controller.SetBearSpeed(bearSpeed);

bearMovement.SetActive(true);
    controller.isActiveInnPool = false;

      //Reset priority
      bearSpawn = 0;

         }else if(currentSpawn == "woodenFenceSpawnValue")
{

    GameObject fence = WoodenFencePool.SharedInstance.GetTrack();

    WoodenFenceController controller = fence.GetComponent<WoodenFenceController>();
  
// spawnNumber == 2, means its first spawn, because of spawnNumber += 1 earlier
// 4 Blocks the row if bear is placed. 
// selectedX and Z is used different and lane number is ignored for Bears, 
//because they have to begin in left lane. 
// spawnNumber is 2 for first row and others for last row, because 1 is already added to the value. 
if(spawnNumber == 2)
                {
                     usedRowOne = 5;
                     fence.transform.position = new Vector3(sectionTwoX, 1.1f, sectionTwoZ);
                }
                else
                {
                    usedRowTwo = 5;
                    fence.transform.position = new Vector3(sectionFiveX, 1.1f, sectionFiveZ);
                }
             if(currentDirection == "North")
                    {
             
                  fence.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
           else if(currentDirection == "East")
                    {
            
                  fence.transform.rotation = Quaternion.Euler(0, 90, 0);
                  
                  }
           else if (currentDirection == "West")
                    {
                
                fence.transform.rotation = Quaternion.Euler(0, -90, 0);

                    }
        
        fence.SetActive(true);
                        
    controller.isActiveInnPool = false;

    controller.SetId(spawnId);

    //Reset priority
      woodenFenceSpawn = 0;
          
            }
            else
            {
                  break;
            }
         
        }

spawnNumber = 1;
usedRowOne = 0;
usedRowTwo = 0;
spawnId += 1;

    }
   
    public void SetCurrentDirection()
    {
        currentDirection = buildDirections[directionValue];
    }

    public void RemoveElementsOnTrack(int id)
    {
        coinPool.FindLinkedElements(id);
        bearPool.FindLinkedElements(id);
        woodenFirePool.FindLinkedElements(id);
        woodenFencePool.FindLinkedElements(id);
    }

    public void Awake()
    {
        coinPool = FindObjectOfType<CoinPool>();
        bearPool = FindObjectOfType<BearPool>();
        woodenFirePool = FindAnyObjectByType<WoodenFirePool>();
        woodenFencePool = FindAnyObjectByType<WoodenFencePool>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }  
}
