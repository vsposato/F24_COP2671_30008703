using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [Header("Difficulty Values")]
    [SerializeField]
    [Tooltip("The value to be used for this button")]
    private int difficulty;

    private GameManager _gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }
}
