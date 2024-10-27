using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    /// <summary>
    /// This class handles pausing and unpausing the game.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        [Header("Menu Screens")]
        [Tooltip("Options Screen UI Element")]
        [SerializeField]
        private GameObject optionsScreen;

        [Tooltip("Help Screen UI Element")]
        [SerializeField]
        private GameObject helpScreen;

        [Tooltip("Pause Screen UI Element")]
        [SerializeField]
        private GameObject pauseScreen;

        /// <summary>
        /// A boolean indicating whether the game is currently paused.
        /// </summary>
        private bool _gameIsPaused;

        /// <summary>
        /// This function is responsible for updating the game state based on the pause control.
        /// It calls the PauseUnpause function every frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.IsGameActive())
            {
                PauseUnpause();
            }
        }

        /// <summary>
        /// This function loads the main menu scene and resets the time scale to 1.
        /// </summary>
        /// <remarks>
        /// This function is called when the user selects the Quit to Main menu item.
        /// It uses the SceneManager to load the main menu scene, which is assumed to be at index 0 in the build settings.
        /// After loading the main menu scene, it sets the time scale back to 1, ensuring that the game runs at its normal speed.
        /// </remarks>
        public void QuitToMain()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Activates the Options Screen UI Element.
        /// </summary>
        /// <remarks>
        /// This function is called when the user selects the Options menu item.
        /// It sets the 'optionsScreen' GameObject to active, making it visible on the screen.
        /// </remarks>
        public void OpenOptions()
        {
            optionsScreen.SetActive(true);
        }

        /// <summary>
        /// Toggles the visibility of the Help Screen UI Element.
        /// </summary>
        /// <remarks>
        /// This function is called when the user selects the Help menu item.
        /// It toggles the 'helpScreen' GameObject's active state, making it visible or hidden on the screen.
        /// If the 'helpScreen' is currently active, it will be deactivated, and vice versa.
        /// </remarks>
        public void ToggleHelpScreen()
        {
            helpScreen.SetActive(!helpScreen.activeSelf);
        }

        /// <summary>
        /// Deactivates the Options Screen UI Element.
        /// </summary>
        /// <remarks>
        /// This function is called when the user selects the Close Options menu item or when the user exits the Options menu.
        /// It sets the 'optionsScreen' GameObject to inactive, making it invisible on the screen.
        /// </remarks>
        public void CloseOptions()
        {
            optionsScreen.SetActive(false);
        }

        /// <summary>
        /// This function handles the pausing and unpausing of the game.
        /// It toggles the pause state, activating or deactivating the pause screen and adjusting the time scale.
        /// </summary>
        public void PauseUnpause()
        {
            if (!_gameIsPaused)
            {
                // If the game is not paused, activate the pause screen and set the time scale to 0.
                pauseScreen.SetActive(true);
                _gameIsPaused = true;

                Time.timeScale = 0f;
            }
            else
            {
                // If the game is paused, deactivate the pause screen and set the time scale back to 1.
                pauseScreen.SetActive(false);
                _gameIsPaused = false;

                Time.timeScale = 1f;
            }
        }
    }
}