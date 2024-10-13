using System;
using UnityEngine;
using UnityEngine.UI;

namespace Stealth
{
    public class PlayerSafeZoneChecker : MonoBehaviour
    {
        public bool PlayerIsInSafeZone = false;
        [SerializeField] private Image detectionIndicator;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SafeZone"))
            {
                PlayerIsInSafeZone = true;
                detectionIndicator.fillAmount = 0;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("SafeZone"))
            {
                PlayerIsInSafeZone = false;

            }
        }
    }
}