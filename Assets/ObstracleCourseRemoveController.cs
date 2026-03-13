using UnityEngine;

public class ObstracleCourseRemoveController : MonoBehaviour
{
    private ObstracleCourseLevelOneController obstracleCourseLevelOneController;
  
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {
Debug.Log("ok");
Debug.Log(playerNinjaCollider.name);
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
obstracleCourseLevelOneController.RemoveUsedTrack();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstracleCourseLevelOneController = GetComponentInParent<ObstracleCourseLevelOneController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
