using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class represents a difficulty button in the game. It is responsible for setting the difficulty level when clicked.
/// </summary>
public class DifficultyButton : MonoBehaviour
{
    [Header("Difficulty Values")]
    [SerializeField]
    [Tooltip("The value to be used for this button")]
    private int difficulty;

    private GameManager _gameManager;


    /// <summary>
    /// This function initializes the DifficultyButton component.
    /// It finds the GameManager object, retrieves its GameManager component,
    /// and adds a listener to the button's onClick event to call the SetDifficulty method.
    /// </summary>
    private void Start()
    {
        // Find the GameManager object in the scene
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Retrieve the Button component attached to this GameObject
        var button = GetComponent<Button>();

        // Add a listener to the button's onClick event to call the SetDifficulty method
        button.onClick.AddListener(SetDifficulty);
    }

    /// <summary>
    /// This method sets the difficulty level in the game when the corresponding difficulty button is clicked.
    /// </summary>
    private void SetDifficulty()
    {
        // Calls the StartGame method of the GameManager component with the difficulty value set in the Inspector.
        _gameManager.StartGame(difficulty);
    }
}