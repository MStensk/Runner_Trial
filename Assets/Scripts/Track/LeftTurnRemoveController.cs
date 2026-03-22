using UnityEngine;

public class LeftTurnRemoveController : MonoBehaviour
{
    private LeftTurnController leftTurnController;
  
     private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
         
         leftTurnController.RemoveUsedTrack();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftTurnController = GetComponentInParent<LeftTurnController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
