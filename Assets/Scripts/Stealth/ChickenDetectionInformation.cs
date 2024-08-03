using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stealth
{
    public class ChickenDetectionInformation : MonoBehaviour
    { 
        public ChickenNavigation[] chickens;
        
        private void OnValidate()
        {
            chickens = FindObjectsOfType<ChickenNavigation>();
        }

        public void OnPlayerDetected()
        {
            chickens.ToList().ForEach(x => x.GoToPlayersSpot(FindObjectOfType<PlayerInput>().transform.position));
        }
    }
}