using UnityEngine;

public class LeftTurnController : MonoBehaviour
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
        generator.BuildTrack();    

           triggerTrackConstruction = false;
        }
    }

 public void RemoveUsedTrack()
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
  
    }
}
