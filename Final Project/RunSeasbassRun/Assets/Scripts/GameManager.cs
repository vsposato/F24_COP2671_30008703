using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The main game manager script that handles game logic, UI updates, and game flow.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Score Text object")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Tooltip("Timer Text object")]
    [SerializeField]
    private TextMeshProUGUI timerText;

    [Tooltip("Game Over Text object")]
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [Tooltip("Restart Button object")]
    [SerializeField]
    private Button restartButton;

    [Tooltip("Title Screen object")]
    [SerializeField]
    private GameObject titleScreen;

    [Tooltip("Main Camera object")]
    [SerializeField]
    private GameObject mainCamera;

    [Tooltip("Player object")]
    [SerializeField]
    private GameObject player;

    private bool _gameOver = true;
    private int _score;
    private int _timer;
    private SpawnManager _spawnManager;
    private AudioSource _mainCameraAudioSource;
    private Animator _playerAnim;
    private float _obstacleSpawnRate = 3.0f;
    private float _coinSpawnRate = 3.0f;

    private readonly Dictionary<float, DifficultyLevelInfo> _difficultyLevels =
        new Dictionary<float, DifficultyLevelInfo>()
        {
            { 1, new DifficultyLevelInfo(4.0f, 2.0f, 60) },
            { 2, new DifficultyLevelInfo(3.0f, 1.5f, 45) },
            { 3, new DifficultyLevelInfo(2.0f, 1.0f, 30) },
        };

    /// <summary>
    /// Initializes the game components and sets up the initial game state.
    /// </summary>
    private void Start()
    {
        // Find and retrieve the SpawnManager component from the "SpawnManager" GameObject
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        // Retrieve the AudioSource component from the mainCamera GameObject
        _mainCameraAudioSource = mainCamera.GetComponent<AudioSource>();

        // Retrieve the Animator component from the player GameObject
        _playerAnim = player.GetComponent<Animator>();

        // Disable the player's Animator component
        _playerAnim.enabled = false;
    }

    /// <summary>
    /// Coroutine that spawns obstacles at regular intervals while the game is active.
    /// </summary>
    /// <returns>An IEnumerator for use with Coroutines.</returns>
    private IEnumerator SpawnTarget()
    {
        // Loop until the game is over
        while (IsGameActive())
        {
            // Wait for the specified obstacle spawn rate before spawning the next obstacle
            yield return new WaitForSeconds(_obstacleSpawnRate);

            // Call the SpawnObstacle method on the SpawnManager to create a new obstacle
            _spawnManager.SpawnObstacle();
        }
    }

    /// <summary>
    /// Coroutine that spawns coins at regular intervals while the game is active.
    /// </summary>
    /// <returns>An IEnumerator for use with Coroutines.</returns>
    private IEnumerator SpawnCoin()
    {
        // Loop until the game is over
        while (IsGameActive())
        {
            // Wait for the specified coin spawn rate before spawning the next coin
            yield return new WaitForSeconds(_coinSpawnRate);

            // Call the SpawnCoin method on the SpawnManager to create a new coin
            _spawnManager.SpawnCoin();
        }
    }

    /// <summary>
    /// Coroutine that counts down the timer and triggers the game over when it reaches zero.
    /// </summary>
    /// <returns>An IEnumerator for use with Coroutines.</returns>
    private IEnumerator CountdownTimer()
    {
        // Loop until the game is active
        while (IsGameActive())
        {
            // Wait for one second before decrementing the timer
            yield return new WaitForSeconds(1);

            // Decrement the timer
            DecrementTimer();

            // Check if the timer has reached zero
            if (_timer == 0)
            {
                // Call the GameOver method to end the game
                GameOver();
            }
        }
    }

    /// <summary>
    /// Updates the player's score by adding the specified amount to the current score.
    /// </summary>
    /// <param name="scoreToAdd">The amount to add to the player's score.</param>
    /// <returns>No return value.</returns>
    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = $"Score: {_score}";
    }

    /// <summary>
    /// Decrements the timer by one and updates the timer text UI.
    /// </summary>
    /// <remarks>
    /// This function is called each second during the game to keep track of the remaining time.
    /// </remarks>
    private void DecrementTimer()
    {
        // Decrement the timer by one & update the timer text UI with the current timer value
        timerText.text = $"Time: {--_timer}";
    }

    /// <summary>
    /// Handles the game over sequence by activating the game over text, restart button, stopping the main camera audio,
    /// disabling the player's animator, and setting the game over status.
    /// </summary>
    public void GameOver(bool isPlayerDead = false)
    {
        // Set the game over status to true
        SetGameOver(true);

        // Activate the game over text
        gameOverText.gameObject.SetActive(true);

        // Activate the restart button
        restartButton.gameObject.SetActive(true);

        // Stop the main camera audio
        _mainCameraAudioSource.Stop();

        if (!isPlayerDead)
        {
            // Disable the player's Animator component
            _playerAnim.enabled = false;
        }
    }

    /// <summary>
    /// Restarts the current game scene by loading it again.
    /// </summary>
    /// <remarks>
    /// This function is called when the Restart button is clicked during the game over sequence.
    /// It resets the game state, score, timer, and other game elements to their initial values.
    /// </remarks>
    public void RestartGame()
    {
        // Load the current active scene again to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Starts the game by initializing game state, spawning obstacles and coins, updating UI, and playing audio.
    /// </summary>
    /// <param name="difficulty">The difficulty level of the game. Higher values increase the spawn rate of obstacles and coins.</param>
    /// <remarks>
    /// This function adjusts the spawn rates of obstacles and coins based on the difficulty level.
    /// It also resets the game state, score, timer, and other game elements to their initial values.
    /// Once the game starts, it spawns obstacles and coins at regular intervals, updates the score and timer UI,
    /// plays the main camera audio, and enables the player's animator.
    /// </remarks>
    public void StartGame(int difficulty)
    {
        // Set GameOver status to false
        SetGameOver(false);
        // Get the difficult level definition for the selected difficulty
        var difficultyLevelInfo = _difficultyLevels[difficulty];
        // Divide the obstacle spawn rate by the difficulty to speed up the obstacles
        _obstacleSpawnRate = difficultyLevelInfo.ObstacleSpawnRate;
        // Multiply the coin spawn rate by the difficulty to slow down the coins
        _coinSpawnRate = difficultyLevelInfo.CoinSpawnRate;
        // Multiply the timer base of 30 seconds by 4 - difficulty
        _timer = difficultyLevelInfo.Timer;
        timerText.text = $"Time: {_timer}";
        // Set score to zero
        _score = 0;

        // Start the CoRoutines for spawning obstacles and coins, and a coroutine for counting
        // down the timer
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnCoin());
        StartCoroutine(CountdownTimer());

        // Deactivate the game over text, restart button, and title screen
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        _mainCameraAudioSource.Play();
        _playerAnim.enabled = true;
    }

    /// <summary>
    /// Checks if the game is currently active.
    /// </summary>
    /// <returns>
    /// Returns true if the game is not over (i.e., the game is active).
    /// Returns false if the game is over.
    /// </returns>
    public bool IsGameActive()
    {
        return !_gameOver;
    }

    /// <summary>
    /// Sets the game over status based on the provided boolean value.
    /// </summary>
    /// <param name="currentStatus">
    /// A boolean value indicating the game over status.
    /// If true, the game is considered over.
    /// If false, the game is considered active.
    /// </param>
    /// <returns>
    /// No return value.
    /// This function simply updates the game over status internally.
    /// </returns>
    private void SetGameOver(bool currentStatus)
    {
        _gameOver = currentStatus;
    }
}