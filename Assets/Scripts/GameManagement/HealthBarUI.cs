using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image fillImage;

    void Update()
    {
        float current = playerHealth.GetCurrentHealth();
        float max = playerHealth.GetMaxHealth();

        fillImage.fillAmount = current / max;
    }
}
