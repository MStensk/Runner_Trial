using UnityEngine;

public class WoodenFenceController : MonoBehaviour
{
    public bool isActiveInnPool;
    public int commonID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public void SetId(int id)
    {
        commonID = id;
    }
     public void DeactivateWoodenFence()
    {
        isActiveInnPool = true;
        gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        SetId(0);
    }
    
    
    void Start()
    {
        isActiveInnPool = false;
    }

    // Update is called once per fram
    void Update()
    {
        
    }
}
