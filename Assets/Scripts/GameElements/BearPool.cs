using UnityEngine;
using System.Collections.Generic;

public class BearPool : MonoBehaviour
{

[SerializeField] private GameObject objectToPool;
public static BearPool SharedInstance;
public List<GameObject> pooledObjects;
public int amountToPool;

public void Awake()
    {
        SharedInstance = this;

        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);

        BearMovement controller = tmp.GetComponent<BearMovement>();
            controller.isActiveInnPool = false;
            pooledObjects.Add(tmp);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    
    }

    public void FindLinkedElements(int id)
{
    
        for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject bear = pooledObjects[i];
if (!bear.activeInHierarchy) continue;

 BearMovement controller = bear.GetComponent<BearMovement>(); 
       
          if(controller.commonId == id)
            {
                //Frezez bears movement in lane, needed for Deactivate to work on bearMovement
                controller.SetRemoveBear();

                controller.DeactivateBear();
                
            break;
            } 
        }
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
