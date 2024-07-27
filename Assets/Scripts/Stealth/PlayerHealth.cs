using System.Collections;
using System.Collections.Generic;
using Moles;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private TextMeshProUGUI healthMonitor;
    [SerializeField] private Animator takeDamageAnim;
    
    public void TakeDamage(int _amount)
    {
        health -= _amount;
        healthMonitor.text = $"{health}/10";
        takeDamageAnim.SetTrigger("Damage");
        if (health <= 0)
        {
            FindObjectOfType<Stealth.WinLoseLogic>().GameOver();
        }
    }
}
