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

    public GameObject GetTrack()
    {/*
 if (pooledObjects == null || pooledObjects.Count == 0)
        return null;  */

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
