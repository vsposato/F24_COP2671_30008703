using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Tooltip("Game status")] [SerializeField]
    private bool gameOver = false;

    private int _score;
    private int _timer;
    private int _difficulty;
    private SpawnManager _spawnManager;
    private float _obstacleSpawnRate = 3.0f;
    private float _coinSpawnRate = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
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
        SetGameOver(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        _difficulty = difficulty;
        _obstacleSpawnRate /= _difficulty;
        _coinSpawnRate *= _difficulty;
        SetGameOver(true);
        _score = 0;
        _timer = 60;

        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnCoin());

        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
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