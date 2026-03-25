using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int currentTrackPiece;
    public bool isActiveInnPool;
    public int commonID;
    public int removeSpeedIdentifier = 0;

    [Header("Coin Effects")]
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float speedBoost = 1f;
    [Header("Danger Reduction")]
    [SerializeField] private float dangerReduction = 1f;
    int levelDurabillity = 10;

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
            player.AddToCoinBank();

            int finalScore = player.GetCoinScoreValue(scoreValue);
            ScoreManager.Instance.AddScore(finalScore);

            player.AddSpeed(speedBoost);
            DangerSystem dangerSystem = FindObjectOfType<DangerSystem>();
            if (dangerSystem != null)
            {
            dangerSystem.ReduceDanger(dangerReduction); // you can tweak this later
            }

            HandleHealthPickup();

            removeSpeedIdentifier = TrackGenerator.Instance.GetSpawnId();

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

     if(weight < 2) return;

// sets loft for how difficoult it is to gain health
     if(weight > 16){ weight = 16; }
// The /2 reduces weigths impact n the propabillity to gain life
     int random = UnityEngine.Random.Range(1, (weight / 2) + 3);

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