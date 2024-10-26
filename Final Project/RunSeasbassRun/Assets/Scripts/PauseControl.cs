using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class handles pausing and unpausing the game.
/// </summary>
public class PauseControl : MonoBehaviour
{
    /// <summary>
    /// A static boolean indicating whether the game is currently paused.
    /// </summary>
    public static bool GameIsPaused;

    [Header("UI Settings")]
    [Tooltip("Game Paused Text object")]
    [SerializeField]
    private TextMeshProUGUI pauseText;

    /// <summary>
    /// This function is responsible for checking if the escape key is pressed and if the game is active.
    /// If both conditions are met, it toggles the game pause state.
    /// </summary>
    private void Update()
    {
        // If the escape key is pressed and the game is active, toggle the game pause state
        if (!Input.GetKeyDown(KeyCode.Escape) || !GameManager.Instance.IsGameActive())
        {
            return;
        }

        GameIsPaused = !GameIsPaused;
        PauseGame();
    }

    /// <summary>
    /// Pauses or unpauses the game based on the current GameIsPaused state.
    /// </summary>
    private void PauseGame()
    {
        if (GameIsPaused)
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