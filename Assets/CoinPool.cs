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
 
 for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject coin = pooledObjects[i];
    CoinController controller = coin.GetComponent<CoinController>(); 
            if(controller.commonID == id)
            {
                controller.DeactivateCoin();
              
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
