using UnityEngine;
using System.Collections.Generic;

public class RigthTurnPool : MonoBehaviour
{
[SerializeField] private GameObject objectToPool;
public static RigthTurnPool SharedInstance;
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

        RigthTurnController controller = tmp.GetComponent<RigthTurnController>();
            controller.isActiveInnPool = false;
            controller.triggerTrackConstruction = false;

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
