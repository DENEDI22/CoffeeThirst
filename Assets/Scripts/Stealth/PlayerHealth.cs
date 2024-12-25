using System.Collections;
using System.Collections.Generic;
using Moles;
using TMPro;
using UnityEngine;
using Utilities;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private TextMeshProUGUI healthMonitor;
    [SerializeField] private Animator takeDamageAnim;
    [SerializeField] private AudioSource damageImpactSoundPlayer;

    [SerializeField] private AudioClip[] audioClips; //temrorary solution. Make it SRP pls
    
    
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

    public void TakeDamage(int _amount, DamageImpactSoundType _soundType) //todo add different damage sounds for different types of damage
    {
        damageImpactSoundPlayer.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        TakeDamage(_amount);
    }
}
