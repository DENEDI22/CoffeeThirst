using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stealth
{
    public class WinLoseLogic : MonoBehaviour
    {
        [SerializeField] private int eggsNeedToWin;
        private int eggCount = 0;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI EggsLeft;
        

        private void Start()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnEggTaken()
        {
            eggCount++;
            EggsLeft.text = (eggsNeedToWin - eggCount).ToString();
            if (eggCount >= eggsNeedToWin)
            {
                Win();
            }
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        public void Win()
        {
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
