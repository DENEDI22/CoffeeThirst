using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        public void TogglePauseMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = Time.timeScale != 0 ? 0 : 1;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}