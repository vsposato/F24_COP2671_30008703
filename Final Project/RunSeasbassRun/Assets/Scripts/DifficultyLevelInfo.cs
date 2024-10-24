public class DifficultyLevelInfo
{
    public float ObstacleSpawnRate { get; set; }
    public float CoinSpawnRate { get; set; }

    public int Timer { get; set; }

    public DifficultyLevelInfo(float obstacleSpawnRate, float coinSpawnRate, int timer)
    {
        ObstacleSpawnRate = obstacleSpawnRate;
        CoinSpawnRate = coinSpawnRate;
        Timer = timer;
    }
}