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
    Debug.Log("Bear have been removed in:  FindLinkedElements 2");
int count = 0;
        for(int i = 0; i < pooledObjects.Count; i++)
        {
     GameObject bear = pooledObjects[i];
if (!bear.activeInHierarchy) continue;
    BearMovement controller = bear.GetComponent<BearMovement>(); 
          
          Debug.Log(
            "Pool index: " + i +
            " | instance: " + bear.GetInstanceID() +
            " | active: " + bear.activeInHierarchy +
            " | commonId: " + controller.commonId
        );
          
          if(controller.commonId == id)
            {
                //Frezez bears movement in lane, needed for Deactivate to work on bearMovement
                controller.SetRemoveBear();

                controller.DeactivateBear();
                
                 Debug.Log("Bear have been removed in:  FindLinkedElements loop 3. Id: " + id);
            break;
            } 
            count ++;
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
