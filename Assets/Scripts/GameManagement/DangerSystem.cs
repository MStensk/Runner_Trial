using UnityEngine;
using UnityEngine.UI;

public class DangerSystem : MonoBehaviour
{
    [Header("Danger Settings")]
    [SerializeField] private float danger = 0f;
    [SerializeField] private float maxDanger = 100f;
    [SerializeField] private float increaseInterval = 0.5f;
    [SerializeField] private float increaseAmount = 1f;
    [SerializeField] private Image dangerFill;
    [Header("Player Health")]
    [SerializeField] private Health playerHealth;
    private float timer;
    private bool isDead = false;

    
    void Start()
    {
        
    }

    
  void Update()
{
    IncreaseDangerOverTime();

    float normalizedDanger = danger / maxDanger;

    // Update fill
    dangerFill.fillAmount = normalizedDanger;

    // Smooth color transition
    Color color;

    if (normalizedDanger < 0.5f)
    {
        // Green → Yellow
        float t = normalizedDanger / 0.5f;
        color = Color.Lerp(Color.green, Color.yellow, t);
    }
    else
    {
        // Yellow → Red
        float t = (normalizedDanger - 0.5f) / 0.5f;
        color = Color.Lerp(Color.yellow, Color.red, t);
    }

    dangerFill.color = color;

    CheckDangerState();
}


 private void IncreaseDangerOverTime()
    {
        timer += Time.deltaTime;

        if (timer >= increaseInterval)
        {
            danger += increaseAmount;
            timer = 0f;

            danger = Mathf.Clamp(danger, 0f, maxDanger);

            Debug.Log("Danger: " + danger);
        }
    }

private void CheckDangerState()
{
    if (danger >= maxDanger && !isDead)
    {
        isDead = true;

        Debug.Log("PLAYER DEAD (Danger reached 100)");

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(9999, gameObject);
        }
    }
}


public void ReduceDanger(float amount)
    {
        danger -= amount;
        danger = Mathf.Clamp(danger, 0f, maxDanger);
    }

    public float GetDanger()
    {
        return danger;
    }




}
