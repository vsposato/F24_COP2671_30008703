using Unity.VisualScripting;
using UnityEngine;
using Utilities;

/// <summary>
/// This class handles spawning of obstacles and coins.
/// </summary>
public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
    [Tooltip("Obstacles to be spawned during the game")]
    [SerializeField]
    private GameObject[] obstaclePrefab;

    [Tooltip("Coin to be spawned during the game")]
    [SerializeField]
    private GameObject coinPrefab;

    [DoNotSerialize]
    public bool spawnInProgress = false;

    private readonly Vector3 _obstacleSpawnPos = new(25, 0, -4);
    private readonly Vector3 _coinSpawnPos = new(25, 0, -3.25f);

    private const float MinCoinSpawnY = 3.75f;
    private const float MaxCoinSpawnY = 7.5f;
    private const float MultipleCoinSpacingX = 1.5f;
    private const int MinCoinSpawnCount = 1;
    private const int MaxCoinSpawnCount = 6;

    /// <summary>
    /// Spawns a random obstacle from the obstaclePrefab array at the _obstacleSpawnPos.
    /// </summary>
    public void SpawnObstacle()
    {
        if (spawnInProgress)
        {
            Logging.PrintWarn("Skipping Obstacle spawn due to coins being spawned");
            return;
        }

        Logging.PrintLog("Spawning Obstacle");
        spawnInProgress = true;
        var obstacleScrollSpeed = GameManager.Instance.DifficultyLevelInfo.ScrollRate;
        var obstacleNumber = Random.Range(0, obstaclePrefab.Length);
        var obstacle = obstaclePrefab[obstacleNumber];
        obstacle.GetComponent<ScrollingObject>().SetScrollSpeed(obstacleScrollSpeed);
        Instantiate(obstacle, _obstacleSpawnPos, obstacle.transform.rotation);
        spawnInProgress = false;
    }

    /// <summary>
    /// Spawns a random number of coins (1-5) from the coinPrefab at random Y positions
    /// around _coinSpawnPos.
    /// </summary>
    public void SpawnCoin()
    {
        if (spawnInProgress)
        {
            Logging.PrintWarn("Skipping Coin spawn due to obstacle being spawned");
            return;
        }

        Logging.PrintLog("Spawning Coin");
        spawnInProgress = true;
        var coinScrollSpeed = GameManager.Instance.DifficultyLevelInfo.ScrollRate;
        var spawnCoins = Random.Range(MinCoinSpawnCount, MaxCoinSpawnCount);
        coinPrefab.GetComponent<ScrollingObject>().SetScrollSpeed(coinScrollSpeed);

        var coinSpawnPos =
            _coinSpawnPos + new Vector3(0, Random.Range(MinCoinSpawnY, MaxCoinSpawnY), 0);
        for (var i = 0; i < spawnCoins; i++)
        {
            coinSpawnPos += new Vector3(MultipleCoinSpacingX, 0, 0);
            Instantiate(coinPrefab, coinSpawnPos, coinPrefab.transform.rotation);
        }

        spawnInProgress = false;
    }
}