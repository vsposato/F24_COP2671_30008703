namespace Models
{
    /// <summary>
    /// Represents the difficulty level information for the game.
    /// </summary>
    public class DifficultyLevelInfo
    {
        /// <summary>
        /// The rate at which obstacles are spawned in the game.
        /// </summary>
        public float ObstacleSpawnRate { get; private set; }

        /// <summary>
        /// The rate at which coins are spawned in the game.
        /// </summary>
        public float CoinSpawnRate { get; private set; }

        /// <summary>
        /// The rate at which the game scrolls.
        /// </summary>
        public float ScrollRate { get; private set; }

        /// <summary>
        /// The timer for the difficulty level.
        /// </summary>
        public int Timer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifficultyLevelInfo"/> class.
        /// </summary>
        /// <param name="obstacleSpawnRate">The rate at which obstacles are spawned in the game.</param>
        /// <param name="coinSpawnRate">The rate at which coins are spawned in the game.</param>
        /// <param name="scrollRate"></param>
        /// <param name="timer">The timer for the difficulty level.</param>
        public DifficultyLevelInfo(float obstacleSpawnRate, float coinSpawnRate, float scrollRate, int timer)
        {
            ObstacleSpawnRate = obstacleSpawnRate;
            CoinSpawnRate = coinSpawnRate;
            ScrollRate = scrollRate;
            Timer = timer;
        }
    }
}