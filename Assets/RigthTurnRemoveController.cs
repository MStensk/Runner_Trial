using UnityEngine;

public class RigthTurnRemoveController : MonoBehaviour
{
    private RigthTurnController rigthTurnController;
  
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
rigthTurnController.RemoveUsedTrack();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigthTurnController = GetComponentInParent<RigthTurnController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
