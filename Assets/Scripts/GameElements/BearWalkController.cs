using UnityEngine;

public class BearWalkController : MonoBehaviour
{
    BearWalkController bearWalkController;
    BearMovement bearMovement;
    BoxCollider walkCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       bearMovement = GetComponentInParent<BearMovement>();
       walkCollider = GetComponent<BoxCollider>();
    }

 // Start is called once before the first execution of Update after the MonoBehaviour is created
      private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
        Debug.Log("Bear WALK activated by ninja");   
           bearMovement.StartBearWalk();
          walkCollider.enabled = false;

          Debug.Log("BearWalk activated and collider deactivated");
    }

    public void ResetCollider()
    {
        walkCollider.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
