using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("Score Text object")] [SerializeField]
    private TextMeshProUGUI scoreText;

    [Tooltip("Timer Text object")] [SerializeField]
    private TextMeshProUGUI timerText;

    [Tooltip("Game Over Text object")] [SerializeField]
    private TextMeshProUGUI gameOverText;

    [Tooltip("Restart Button object")] [SerializeField]
    private Button restartButton;

    [Tooltip("Title Screen object")] [SerializeField]
    private GameObject titleScreen;

    [Tooltip("Main Camera object")] [SerializeField]
    private GameObject mainCamera;

    [Tooltip("Player object")] [SerializeField]
    private GameObject player;

    [Tooltip("Game status")] [SerializeField]
    private bool gameOver = true;

    private int _score;
    private int _timer;
    private int _difficulty;
    private SpawnManager _spawnManager;
    private AudioSource _mainCameraAudioSource;
    private Animator _playerAnim;
    private float _obstacleSpawnRate = 3.0f;
    private float _coinSpawnRate = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _mainCameraAudioSource = mainCamera.GetComponent<AudioSource>();
        _playerAnim = player.GetComponent<Animator>();
        _playerAnim.enabled = false;

    }

    private IEnumerator SpawnTarget()
    {
        while (IsGameActive())
        {
            yield return new WaitForSeconds(_obstacleSpawnRate);
            _spawnManager.SpawnObstacle();
        }
    }
    private IEnumerator SpawnCoin()
    {
        while (IsGameActive())
        {
            yield return new WaitForSeconds(_coinSpawnRate);
            _spawnManager.SpawnCoin();
        }
    }

    private IEnumerator CountdownTimer()
    {
        while (IsGameActive())
        {
            yield return new WaitForSeconds(1);
            DecrementTimer();
            if (_timer == 0)
            {
                Debug.Log("Timed out end game");
                GameOver();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = $"Score: {_score}";
    }

    public void DecrementTimer()
    {
        _timer--;
        timerText.text = $"Time: {_timer}";
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        _mainCameraAudioSource.Stop();
        _playerAnim.enabled = false;
        SetGameOver(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        Debug.Log("Starting Game");

        _difficulty = difficulty;
        Debug.Log($"Difficulty {_difficulty}");
        _obstacleSpawnRate /= _difficulty;
        Debug.Log($"Obstacle Spawn Rate {_obstacleSpawnRate}");
        _coinSpawnRate *= _difficulty;
        Debug.Log($"Coin Spawn Rate {_coinSpawnRate}");
        SetGameOver(false);
        _score = 0;
        _timer = 60;

        Debug.Log("Running coroutines");
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnCoin());
        StartCoroutine(CountdownTimer());

        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        _mainCameraAudioSource.Play();
        _playerAnim.enabled = true;
    }

    public bool IsGameActive()
    {
        return !gameOver;
    }

    public void SetGameOver(bool currentStatus)
    {
        gameOver = currentStatus;
    }
}