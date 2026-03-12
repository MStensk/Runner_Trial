using UnityEngine;
using System.Collections.Generic;

public class LeftCornerPiecesPool : MonoBehaviour
{
[SerializeField] private GameObject objectToPool;
public static LeftCornerPiecesPool SharedInstance;
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

        LeftCornerController controller = tmp.GetComponent<LeftCornerController>();
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
