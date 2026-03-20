using UnityEngine;
using System.Collections.Generic;

public class WoodenFencePool : MonoBehaviour
{

[SerializeField] private GameObject objectToPool;
public static WoodenFencePool SharedInstance;
public List<GameObject> pooledObjects;
public int amountToPool;

public void FindLinkedElements(int id)
{
 
 for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject fence = pooledObjects[i];
    WoodenFenceController controller = fence.GetComponent<WoodenFenceController>(); 
            if(controller.commonID == id)
            {
                controller.DeactivateWoodenFence();
              
}
    }
    }  

public void Awake()
    {
        SharedInstance = this;

        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);

        WoodenFenceController controller = tmp.GetComponent<WoodenFenceController>();
            controller.isActiveInnPool = false;
            pooledObjects.Add(tmp);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    
    }
    public GameObject GetTrack()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            } 
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
