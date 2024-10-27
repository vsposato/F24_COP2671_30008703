using UnityEditor;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject optionsScreen;

        public void StartGame()
        {
        }

        public void OpenOptions()
        {
            optionsScreen.SetActive(true);
        }

        public void CloseOptions()
        {
            optionsScreen.SetActive(false);
        }

        public void OpenHelp()
        {
        }

        public void CloseHelp()
        {
        }

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