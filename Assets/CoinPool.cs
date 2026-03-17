using UnityEngine;
using System.Collections.Generic;

public class CoinPool : MonoBehaviour
{

[SerializeField] private GameObject objectToPool;
public static CoinPool SharedInstance;
public List<GameObject> pooledObjects;
public int amountToPool;

public void FindLinkedElements(int id)
{
    Debug.Log("Coin have been removed in:  FindLinkedElements 2");

        for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject coin = pooledObjects[i];
    CoinController controller = coin.GetComponent<CoinController>(); 
            if(controller.commonID == id)
            {
                controller.DeactivateCoin();
                Debug.Log("Coin have been removed in:  FindLinkedElements loop 3");
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

        CoinController controller = tmp.GetComponent<CoinController>();
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
Debug.Log("amountToPool skal være lavest" + amountToPool);
Debug.Log("pooledObjects skal være højest" + pooledObjects.Count);

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
