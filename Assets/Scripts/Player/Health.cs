using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth; 
    [SerializeField] private int maxHealth = 100;
    
    void OnEnable()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        if (attacker == gameObject) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            this.Die(attacker);
            GameOverManager.Instance.GameOver();
        }
    }

    private void Die(GameObject attacker)
    {
       /* IDestructible[] destructibles = GetComponentsInChildren<IDestructible>(); 
        foreach(IDestructible d in destructibles)
        {
            d.OnDestroy(attacker); 
            */
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
