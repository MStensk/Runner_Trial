using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private EndlessRunController player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
{
    if (scoreText == null) return;

    int multiplier = player != null ? player.GetCoinScoreMultiplier() : 1;

    if (multiplier > 1)
    {
        scoreText.text = "Score: " + score + "  x" + multiplier;
    }
    else
    {
        scoreText.text = "Score: " + score;
    }
}

}