using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public static Health Instance;
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
Debug.Log("Attack Name:  " + attacker );


        currentHealth -= damage;
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }

        if (currentHealth <= 0)
        {
            this.Die(attacker);
            GameOverManager.Instance.GameOver();
        }

    }

    private void Die(GameObject attacker)
    {
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

    public void AddHealth()
    {
        if(currentHealth > 0 && currentHealth <= 99)
        currentHealth += 2;
    }

    public void Awake()
    {
      Instance = this;
    }

}
