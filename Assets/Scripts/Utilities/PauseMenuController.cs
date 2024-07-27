using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private AudioSource m_MusicSource;
        
        public void TogglePauseMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            // Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = Time.timeScale != 0 ? 0 : 1;
            if (Time.timeScale > 0)
            {
                m_MusicSource.UnPause();
            }
            else
            {
                m_MusicSource.Pause();
            }
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}