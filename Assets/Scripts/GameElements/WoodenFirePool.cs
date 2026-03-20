using UnityEngine;
using System.Collections.Generic;

public class WoodenFirePool : MonoBehaviour
{

[SerializeField] private GameObject objectToPool;
public static WoodenFirePool SharedInstance;
public List<GameObject> pooledObjects;
public int amountToPool;

public void FindLinkedElements(int id)
{
 
 for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject fire = pooledObjects[i];
    WoodenFireController controller = fire.GetComponent<WoodenFireController>(); 
            if(controller.commonID == id)
            {
                controller.DeactivateFire();
              
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

        WoodenFireController controller = tmp.GetComponent<WoodenFireController>();
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
