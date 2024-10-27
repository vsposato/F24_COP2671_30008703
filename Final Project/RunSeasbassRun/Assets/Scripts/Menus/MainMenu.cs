using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Menu Screens")]
        [Tooltip("Options Screen UI Element")]
        [SerializeField]
        private GameObject optionsScreen;

        [Tooltip("Help Screen UI Element")]
        [SerializeField]
        private GameObject helpScreen;

        public void StartGame()
        {
            SceneManager.LoadScene(1);
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
        /// Exits the game.
        /// </summary>
        /// <remarks>
        /// This function is called when the user selects the Quit Game menu item.
        /// It checks the current platform and performs the appropriate action to exit the game.
        /// In the Unity Editor, it exits playmode. In a standalone build, it quits the application.
        /// </remarks>
        public void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    }
}