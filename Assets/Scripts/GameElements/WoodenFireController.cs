using UnityEngine;

public class WoodenFireController : MonoBehaviour
{
public int currentTrackPiece;
public bool isActiveInnPool;
public int commonID;

public void SetId(int id)
    {
        commonID = id;
    }

     public void SetCurrentTrackPiece(int level)
    {
        currentTrackPiece = level;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
            DeactivateFire();
      
    }

    public void DeactivateFire()
    {
                isActiveInnPool = true;
                gameObject.SetActive(false);
                currentTrackPiece = 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                SetId(0);

    }
    
    void Start()
    {
       isActiveInnPool = false; 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
