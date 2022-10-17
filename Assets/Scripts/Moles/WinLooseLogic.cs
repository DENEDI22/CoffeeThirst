using System;
using UnityEngine;

namespace Moles
{
    internal class WinLooseLogic : MonoBehaviour
    {
        [SerializeField] private GameObject loosePanel;
        [SerializeField] private GameObject winPanel;
        
        public void Win()
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void Loose()
        {
            loosePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}