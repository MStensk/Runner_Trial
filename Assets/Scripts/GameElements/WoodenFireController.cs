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
