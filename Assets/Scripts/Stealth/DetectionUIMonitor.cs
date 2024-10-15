using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Stealth
{
    public class DetectionUIMonitor : MonoBehaviour
    {
        [SerializeField] private Image detectionProgressBar;
        [SerializeField] private int amountChickenSeingPlayer;

        public void SetDetectingValue(float _detectingValue)
        {
            detectionProgressBar.fillAmount = detectionProgressBar.fillAmount < _detectingValue
                ? _detectingValue
                : detectionProgressBar.fillAmount;
        }

        public void SetToDetected() => amountChickenSeingPlayer++;

        public void PlayerLost()
        {
            amountChickenSeingPlayer--;
            if (amountChickenSeingPlayer < 1)
            {
                detectionProgressBar.fillAmount = 0;
            }
        }
        
        public void ResetDetection() //I don't really know what I need it for... May delete
        {
            amountChickenSeingPlayer = 0;
            detectionProgressBar.fillAmount = 0;
        }
    }
}