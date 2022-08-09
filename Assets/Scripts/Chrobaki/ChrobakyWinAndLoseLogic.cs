using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chrobaki
{
    public class ChrobakyWinAndLoseLogic : MonoBehaviour
    {
        [SerializeField] private GameObject gameWinPanel, gameLoosePanel;
        
        public void RestartLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
        public void GameWin()
        {
            Time.timeScale = 0;
            gameWinPanel.SetActive(true);
        }

        public void GameLoose()
        {
            Time.timeScale = 0;
            gameLoosePanel.SetActive(true);
        }
    }
}