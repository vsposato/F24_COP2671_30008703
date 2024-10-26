using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

/// <summary>
/// This class handles pausing and unpausing the game.
/// </summary>
public class PauseControl : MonoBehaviour
{
    /// <summary>
    /// A boolean indicating whether the game is currently paused.
    /// </summary>
    private bool _gameIsPaused;

    [Header("UI Settings")]
    [Tooltip("Game Paused Text object")]
    [SerializeField]
    private TextMeshProUGUI pauseText;


    /// <summary>
    /// This function is responsible for updating the game state based on the pause control.
    /// It calls the HandlePause function every frame.
    /// </summary>
    private void Update()
    {
        HandlePause();
    }

    /// <summary>
    /// This function handles the pausing and unpausing of the game based on the escape key press and game activity.
    /// </summary>
    private void HandlePause()
    {
        // If the escape key is pressed and the game is active, toggle the game pause state
        if (!Input.GetKeyDown(KeyCode.Escape) || !GameManager.Instance.IsGameActive())
        {
            return;
        }

        // Toggle the game pause state
        _gameIsPaused = !_gameIsPaused;

        // Call the PauseGame function to pause or unpause the game
        PauseGame();
    }

    /// <summary>
    /// Pauses or unpauses the game based on the current GameIsPaused state.
    /// </summary>
    private void PauseGame()
    {
        if (_gameIsPaused)
        {
            // Pause the game by setting the timescale to 0
            Time.timeScale = 0f;
            // Pause audio
            AudioListener.pause = true;
            // Display the pause text
            pauseText.gameObject.SetActive(true);
        }
        else
        {
            // Unpause the game by setting the timescale to 1
            Time.timeScale = 1.0f;
            // Unpause audio
            AudioListener.pause = false;
            // Hide the pause text
            pauseText.gameObject.SetActive(false);
        }
    }
}