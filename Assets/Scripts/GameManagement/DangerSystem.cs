using UnityEngine;

public class DangerSystem : MonoBehaviour
{
    [Header("Danger Settings")]
    [SerializeField] private float danger = 0f;
    [SerializeField] private float maxDanger = 100f;
    [SerializeField] private float increaseInterval = 0.5f;
    [SerializeField] private float increaseAmount = 1f;
    private float timer;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        IncreaseDangerOverTime();
        Debug.Log("Current Danger: " + danger);
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
        if (danger >= maxDanger)
        {
            Debug.Log("PLAYER DEAD (Danger reached 100)");
            // TODO: Call death logic
        }
        else if (danger >= 75f)
        {
            Debug.Log("DANGER RED");
        }
        else if (danger >= 50f)
        {
            Debug.Log("DANGER YELLOW");
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
