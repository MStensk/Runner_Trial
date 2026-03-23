using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int currentTrackPiece;
    public bool isActiveInnPool;
    public int commonID;

    [Header("Coin Effects")]
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float speedBoost = 1f;

    public void SetId(int id)
    {
        commonID = id;
    }

    private void OnTriggerEnter(Collider playerNinjaCollider)
    {
        if (!playerNinjaCollider.CompareTag("PlayerNinja")) return;

        EndlessRunController player = playerNinjaCollider.GetComponent<EndlessRunController>();
        if (player != null)
        {
            int finalScore = player.GetCoinScoreValue(scoreValue);
            ScoreManager.Instance.AddScore(finalScore);

            player.AddSpeed(speedBoost);

            HandleHealthPickup();

        }

        DeactivateCoin();
    }

    public void DeactivateCoin()
    {
        isActiveInnPool = true;
        gameObject.SetActive(false);
        currentTrackPiece = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        SetId(0);
    }

    public void HandleHealthPickup()
    {

     int weight = FindHealthBoostOnCurrentLevel();

     int random = UnityEngine.Random.Range(1, weight + 3);

 if (Health.Instance != null && random > weight)
    {
        Health.Instance.AddHealth();
    }
    }

    public int FindHealthBoostOnCurrentLevel()
    {

       float gameLevel = TrackGenerator.Instance.GetGameLevel();

        float result = Mathf.Sqrt(gameLevel);

        return Mathf.CeilToInt(result);
    
    }
    

    void Start()
    {
        isActiveInnPool = false;
    }

    void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }
}