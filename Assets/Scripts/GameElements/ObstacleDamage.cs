using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    [SerializeField] private int damage = 20;
/*-

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();

        EndlessRunController player = other.GetComponent<EndlessRunController>();

        if (player != null)
        {
            player.ResetSpeedAndMultiplier();
        }  

        player.ResetSpeedAndMultiplier();

        if (health != null)
        {
            Debug.Log("Player hit by obstacle");
            health.TakeDamage(damage, gameObject);
        }
    
    }  */


    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();

        EndlessRunController player = other.GetComponent<EndlessRunController>();

      //  BearMovement bear = GetComponent<BearMovement>();
BearMovement bear = GetComponentInParent<BearMovement>();
        Debug.Log(other.name);
Debug.Log(other.transform.root.name);

if (player == null) return;
      
if(bear != null)
        {

float xDistance = Mathf.Abs(bear.transform.position.x - player.transform.position.x);
float zDistance = Mathf.Abs(bear.transform.position.z - player.transform.position.z);

            if (player == null) return;
            if(xDistance < 5 && zDistance < 5)
            {
               player.ResetSpeedAndMultiplier();
               Debug.Log("Player hit by bear obstacle");

        if (health != null)
        {
    
            health.TakeDamage(damage, gameObject);
        } 
            }
        }
else if(bear == null){
        if (player != null)
        {
            player.ResetSpeedAndMultiplier();
        }  

        player.ResetSpeedAndMultiplier();

        if (health != null)
        {
            Debug.Log("Player hit by obstacle");
            health.TakeDamage(damage, gameObject);
        }
    }
    }



}  
