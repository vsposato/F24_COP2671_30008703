using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Obstacles to be spawned during the game")] [SerializeField]
    private GameObject[] obstaclePrefab;

    private readonly Vector3 _obstacleSpawnPos = new Vector3(25, 0, -4);

    [Tooltip("Coin to be spawned during the game")] [SerializeField]
    private GameObject coinPrefab;

    private readonly Vector3 _coinSpawnPos = new Vector3(25, 0, -3.25f);

    private GameManager _gameManagerScript;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void SpawnObstacle()
    {
        var obstacleScrollSpeed = Random.Range(-7.0f, -8.0f);
        var obstacleNumber = Random.Range(0, obstaclePrefab.Length);
        var obstacle = obstaclePrefab[obstacleNumber];
        obstacle.GetComponent<ScrollingObject>().SetScrollSpeed(obstacleScrollSpeed);
        Instantiate(obstacle, _obstacleSpawnPos, obstacle.transform.rotation);
    }

    public void SpawnCoin()
    {
        var coinScrollSpeed = Random.Range(-7.0f, -8.0f);
        var spawnCoins = Random.Range(1, 4);
        coinPrefab.GetComponent<ScrollingObject>().SetScrollSpeed(coinScrollSpeed);
        var coinSpawnPos = _coinSpawnPos + new Vector3(0, Random.Range(1, 7.5f), 0);
        for (var i = 0; i < spawnCoins; i++)
        {
            coinSpawnPos += new Vector3(i * 1.5f, 0, 0);
            Instantiate(coinPrefab, coinSpawnPos, coinPrefab.transform.rotation);
        }
    }
}