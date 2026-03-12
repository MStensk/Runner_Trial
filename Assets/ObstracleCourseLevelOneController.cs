using JetBrains.Annotations;
using UnityEngine;

public class ObstracleCourseLevelOneController : MonoBehaviour
{
     public bool isActiveInnPool;
     public bool triggerTrackConstruction;
     public float lengthOnX = 15f;
     public float lengthOnZ = 30f;

     private Transform character;
 
private void OnTriggerEnter(Collider playerNinjaCollider)
    {
//Debug.Log("ok");
//Debug.Log(playerNinjaCollider.name);
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        if (triggerTrackConstruction)
        {
        TrackGenerator generator = FindObjectOfType<TrackGenerator>();
        generator.BuildTrack();    
        }
    }

    public void RemoveUsedTrack()
    {
  TrackGenerator generator = FindObjectOfType<TrackGenerator>();
        string direction = generator.currentDirection;
Debug.Log("Enter remove straight track");
        if(!isActiveInnPool)
        { 
            Debug.Log(" remove straight track  Is in Scene");
            float zPlacement = transform.position.z;
             float xPlacement =transform.position.x;
         if(direction == "North")
         {    
             Debug.Log(" remove straight track  Is in Scene  North direction");
          // Gammel remove: if(character.position.z > zPlacement + lengthOnZ + 20 || 
          //  character.position.x > xPlacement + 20 && character.position.z > zPlacement + lengthOnZ)
          if(character.position.z > zPlacement + lengthOnZ + 20 || character.position.x > xPlacement + 35 || character.position.x < xPlacement - 35) 
            {
        SetUsedTrack();
Debug.Log(" remove straight track  Is in Scene  North direction Track is set");
              //  isActiveInnPool = true;
              //  triggerTrackConstruction = false;
              //  gameObject.SetActive(false);
              //  transform.rotation = Quaternion.Euler(0, 0, 0);
              return;
            }
         }
         else if(direction == "East")
            {
                  Debug.Log(" remove straight track  Is in Scene East direction");
                 if(character.position.x > xPlacement + lengthOnX + 20 || character.position.z > zPlacement + 35 || character.position.z < zPlacement - 35)
            {
        SetUsedTrack();
        Debug.Log(" remove straight track  Is in Scene  East direction Track is set");
        return;
            }
            }
              else if(direction == "South")
            {
                  Debug.Log(" remove straight track  Is in Scene  South direction");
                 if(character.position.z < zPlacement - lengthOnZ - 20 || character.position.x > xPlacement + 35 || character.position.x < xPlacement - 35)
            {
        SetUsedTrack();
        Debug.Log(" remove straight track  Is in Scene  South direction Track is set");
        return;
            }
            }
                   else if(direction == "West")
            {
                  Debug.Log(" remove straight track  Is in Scene  West direction");
                 if(character.position.x < xPlacement - lengthOnX - 20 || character.position.z > zPlacement + 35 || character.position.z < zPlacement - 35)
            {
        SetUsedTrack();
        Debug.Log(" remove straight track  Is in Scene  West direction Track is set");
        return;
            }
            }
    }
    }

 public void SetUsedTrack()
        {
               isActiveInnPool = true;
                triggerTrackConstruction = false;
                gameObject.SetActive(false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }

 // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
       GameObject characterObject = GameObject.FindWithTag("PlayerNinja");

          if(characterObject != null)
    {
        character = characterObject.transform;
    }
    }

    // Update is called once per frame
    void Update()
    {
   // RemoveUsedTrack();
    }  
}
