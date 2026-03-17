using JetBrains.Annotations;
using UnityEngine;

public class ObstracleCourseLevelOneController : MonoBehaviour
{
     public bool isActiveInnPool;
     public bool triggerTrackConstruction;
     public float lengthOnX = 20.4f;
     public float lengthOnZ = 24f;
     private Transform character;
     public int currentTrackPiece;
     public int commonId;
   public void SetCurrentTrackPiece(int level)
    {
        currentTrackPiece = level;
    }
public void SetId(int id)
    {
        commonId = id;
    }
 
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
                isActiveInnPool = true;
                triggerTrackConstruction = false;
                gameObject.SetActive(false);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                // Kalde metode i generate track med sit: currentTrackPiece værdi
                // Der kalder pools med nmmeret for at deaktivere tilhørende game objekter 
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
