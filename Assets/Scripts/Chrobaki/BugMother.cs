using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Chrobaki
{
    public class BugMother : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private float currentHealth, maxHealth = 100f;
        
        public void UpdateHealthBar()
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        
        public void Hit(float _damage)
        {
            currentHealth -= _damage;
            UpdateHealthBar();
            if (currentHealth<=0)
            {
                FindObjectOfType<ChrobakyWinAndLoseLogic>().GameWin();
            }
        }
    }
}