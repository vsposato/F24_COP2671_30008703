using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Obstacles to be spawned during the game")] [SerializeField]
    private GameObject[] obstaclePrefab;

    private readonly Vector3 _obstacleSpawnPos = new Vector3(25, 0, -4);

    [Tooltip("Coin to be spawned during the game")] [SerializeField]
    private GameObject coinPrefab;

    private readonly Vector3 _coinSpawnPos = new Vector3(25, 0, -3.25f);

    private const float MinScrollSpeed = -7.0f;
    private const float MaxScrollSpeed = -8.0f;
    private const float MinCoinSpawnY = 3.5f;
    private const float MaxCoinSpawnY = 7.5f;
    private const float MultipleCoinSpacingX = 1.5f;

    /// <summary>
    /// Spawns a random obstacle from the obstaclePrefab array at the _obstacleSpawnPos.
    /// </summary>
    public void SpawnObstacle()
    {
        var obstacleScrollSpeed = Random.Range(MinScrollSpeed, MaxScrollSpeed);
        var obstacleNumber = Random.Range(0, obstaclePrefab.Length);
        var obstacle = obstaclePrefab[obstacleNumber];
        obstacle.GetComponent<ScrollingObject>().SetScrollSpeed(obstacleScrollSpeed);
        Instantiate(obstacle, _obstacleSpawnPos, obstacle.transform.rotation);
    }

    /// <summary>
    /// Spawns a random number of coins (1-3) from the coinPrefab at random Y positions around _coinSpawnPos.
    /// </summary>
    public void SpawnCoin()
    {
        var coinScrollSpeed = Random.Range(MinScrollSpeed, MaxScrollSpeed);
        var spawnCoins = Random.Range(1, 4);
        coinPrefab.GetComponent<ScrollingObject>().SetScrollSpeed(coinScrollSpeed);

        var coinSpawnPos =
            _coinSpawnPos + new Vector3(0, Random.Range(MinCoinSpawnY, MaxCoinSpawnY), 0);
        for (var i = 0; i < spawnCoins; i++)
        {
            coinSpawnPos += new Vector3(i * MultipleCoinSpacingX, 0, 0);
            Instantiate(coinPrefab, coinSpawnPos, coinPrefab.transform.rotation);
        }
    }
}