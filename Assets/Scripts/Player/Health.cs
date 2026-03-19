using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth; 
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hitSound;
    
    void OnEnable()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        if (attacker == gameObject) return;

        currentHealth -= damage;
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }

        if (currentHealth <= 0)
        {
            this.Die(attacker);
        }
    }

    private void Die(GameObject attacker)
    {
       /* IDestructible[] destructibles = GetComponentsInChildren<IDestructible>(); 
        foreach(IDestructible d in destructibles)
        {
            d.OnDestroy(attacker); 
            */
        if (musicSource != null)
        {
            musicSource.Stop();
        }
          if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        Destroy(gameObject); 
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
