using UnityEngine;

public class CoinController : MonoBehaviour
{
public int currentTrackPiece;
public bool isActiveInnPool;

 
     public void SetCurrentTrackPiece(int level)
    {
        currentTrackPiece = level;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      private void OnTriggerEnter(Collider playerNinjaCollider)
    {
//Debug.Log("ok");
//Debug.Log(playerNinjaCollider.name);
        
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;
            DeactivateCoin();
            Debug.Log("Coin have been deactivated");
    }

    public void DeactivateCoin()
    {
                isActiveInnPool = true;
                gameObject.SetActive(false);
                currentTrackPiece = 0;
               // transform.rotation = Quaternion.Euler(0, 0, 0);

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
