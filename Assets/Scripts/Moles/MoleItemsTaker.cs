using System;
using UnityEngine;

namespace Moles
{
    public class MoleItemsTaker : MonoBehaviour
    {
        public int score = 0;
        public int scoreGoal = 10;
        [SerializeField] [HideInInspector] private WinLooseLogic m_winLooseLogic;

        private void OnValidate()
        {
            try
            {
                m_winLooseLogic = FindObjectOfType<WinLooseLogic>();
            }
            catch (Exception e)
            {
                Console.WriteLine("There must be a component Moles.WinLooseLogic on the scene");
                throw;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MolesItem molesItem))
            {
                score++;
                molesItem.gameObject.SetActive(false); 
            }
        }
    }
}