using UnityEngine;

public class ObstracleCourseRemoveController : MonoBehaviour
{
    private ObstracleCourseLevelOneController obstracleCourseLevelOneController;

    TrackGenerator trackGenerator;
    public int activeId;
  
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {
    
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
obstracleCourseLevelOneController.RemoveUsedTrack();

activeId = obstracleCourseLevelOneController.commonId;
trackGenerator.RemoveElementsOnTrack(activeId);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstracleCourseLevelOneController = GetComponentInParent<ObstracleCourseLevelOneController>();
        trackGenerator = FindObjectOfType<TrackGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
