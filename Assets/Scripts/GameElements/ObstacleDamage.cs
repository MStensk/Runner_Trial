using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();

        EndlessRunController player = other.GetComponent<EndlessRunController>();
        if (player != null)
        {
            player.ResetSpeedAndMultiplier();
        }

        if (health != null)
        {
            Debug.Log("Player hit by obstacle");
            health.TakeDamage(damage, gameObject);
        }
    }
}