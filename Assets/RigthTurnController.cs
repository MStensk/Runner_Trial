using UnityEngine;

public class RigthTurnController : MonoBehaviour
{

     public bool isActiveInnPool;
     public bool triggerTrackConstruction;
     public float lengthOnX = 41.5f;
     public float lengthOnZ = 45.5f;

     private Transform character;

     private void OnTriggerEnter(Collider playerNinjaCollider)
    {
Debug.Log("ok");
Debug.Log(playerNinjaCollider.name);
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        if (triggerTrackConstruction)
        {
        TrackGenerator generator = FindObjectOfType<TrackGenerator>();

Debug.Log("Rigth turn have triggered track build");

        generator.BuildTrack();    
        }
    }

 public void RemoveUsedTrack()
    {
        if(!isActiveInnPool)
        { 
            float zPlacement = transform.position.z;
             float xPlacement =transform.position.x;
            if(character.position.z > zPlacement + lengthOnZ + 20 || 
            character.position.x > xPlacement + 20 && character.position.z > zPlacement + lengthOnZ)
            {
                isActiveInnPool = true;
                triggerTrackConstruction = false;
                gameObject.SetActive(false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
    }
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
   //     RemoveUsedTrack();
    }
}
