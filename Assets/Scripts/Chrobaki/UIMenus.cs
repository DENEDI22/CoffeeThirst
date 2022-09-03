using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chrobaki
{
    public class UIMenus : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;

        private void Start()
        {
            Time.timeScale = 0;
            if (PlayerPrefs.GetInt("IsRestart") == 1)
            {
                startGameButton.onClick.Invoke();
                PlayerPrefs.SetInt("IsRestart", 0);
            }
        }

        public void ToggleTimeScale()
        {
            Time.timeScale = Time.timeScale < 1 ? 1 : 0;
        }

        public void ReloadLevel()
        {
            PlayerPrefs.SetInt("IsRestart", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            PlayerPrefs.SetInt("IsRestart", 0);
            Application.Quit();
        }
    }
}