using UnityEngine;

public class LeftTurnColliderScript : MonoBehaviour
{
    private LeftTurnController leftTurnController;
    private TrackGenerator trackGenerator;
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {

        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        if (leftTurnController.triggerTrackConstruction)
        {
        TrackGenerator generator = FindObjectOfType<TrackGenerator>();

        generator.BuildTrack();    
//Nyt for at stoppe gentagne builds.
        leftTurnController.triggerTrackConstruction = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         leftTurnController = GetComponentInParent<LeftTurnController>();
        trackGenerator = FindObjectOfType<TrackGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
