using System;
using UnityEngine;

namespace Stealth
{
    public class ChickenEyesChanger : MonoBehaviour
    {
        [SerializeField] private GameObject eyeMesh;
        [SerializeField] private ChickenNavigation chickenNavigation;

        private void OnValidate()
        {
            chickenNavigation = GetComponent<ChickenNavigation>();
        }

        public void FixedUpdate()
        {
            if (chickenNavigation.GetCurrentState != ChickenStates.Patroling)
            {
                eyeMesh.SetActive(true);
            }
            else
            {
                eyeMesh.SetActive(false);
            }
        }
    }
}