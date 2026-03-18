using UnityEngine;

public class RigthTurnColliderScript : MonoBehaviour
{
    private RigthTurnController rightTurnController;
    private TrackGenerator trackGenerator;
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {

Debug.Log("Ninja name in Rigth controller" +playerNinjaCollider.name);
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        if (rightTurnController.triggerTrackConstruction)
        {
        TrackGenerator generator = FindObjectOfType<TrackGenerator>();

Debug.Log("Rigth turn have triggered track build");

        generator.BuildTrack();    
//Nyt for at stoppe gentagne builds.
        rightTurnController.triggerTrackConstruction = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         rightTurnController = GetComponentInParent<RigthTurnController>();
        trackGenerator = FindObjectOfType<TrackGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
