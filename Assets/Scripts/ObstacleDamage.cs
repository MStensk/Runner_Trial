using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponentInParent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage, gameObject);
        }
    }
}