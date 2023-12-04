using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Moles
{
    internal class WinLooseLogic : MonoBehaviour
    {
        [SerializeField] private GameObject lossPanel;
        [SerializeField] private GameObject winPanel;
        private int score;
        [SerializeField] private int winScore;
        [SerializeField] private TextMeshProUGUI objectsLeftCounter;

        private void Start()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Win()
        {
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GameObject.Destroy(FindObjectOfType<PauseMenuController>());
            Time.timeScale = 0;
        }

        public void CatchMoleObject()
        {
            score++;
            objectsLeftCounter.text = $"{winScore-score} objects left";
            if (score >= winScore)
            {
                Win();
            }
        }
        
        //Call it from Utilities.UITimer
        public void Loose()
        {
            lossPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GameObject.Destroy(FindObjectOfType<PauseMenuController>());
            Time.timeScale = 0;
        }
        
        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}