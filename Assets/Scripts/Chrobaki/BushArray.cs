using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chrobaki
{
    public class BushArray : MonoBehaviour
    {
        [SerializeField] public List<Bush> bushes;
        [SerializeField] private Image healthBar;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        [SerializeField] private ChrobakyWinAndLoseLogic chrobakyWinAndLoseLogic;
        

        public void OnValidate()
        {
            bushes.Clear();
            bushes.AddRange(GetComponentsInChildren<Bush>());
            chrobakyWinAndLoseLogic = FindObjectOfType<ChrobakyWinAndLoseLogic>();
            maxHealth = 0;
            for (var index = 0; index < bushes.Count; index++)
            {
                maxHealth += bushes[index].bushMaxHealth;
            }
        }

        public float AverageHealth()
        {
            currentHealth = 0;
            for (var index = 0; index < bushes.Count; index++)
            {
                currentHealth += bushes[index].bushHealth;
            }
            return (float)currentHealth / (float)maxHealth;
        }
        
        
        public void Update()
        {
            healthBar.fillAmount = AverageHealth();
            if(AverageHealth() <= 0) chrobakyWinAndLoseLogic.GameLoose();
        }
    }
}