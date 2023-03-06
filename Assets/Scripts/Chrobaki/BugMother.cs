using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Chrobaki
{
    public class BugMother : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private float currentHealth, maxHealth = 100f;
        [SerializeField] public Transform playerMarker;
        [SerializeField] [HideInInspector] private Transform player;

        [HideInInspector] public bool isUp;

        public void Update()
        {
            if (isUp)
            {
                playerMarker.SetPositionAndRotation(player.transform.position, Quaternion.identity);
                playerMarker.LookAt(new Vector3(transform.position.x, playerMarker.position.y, transform.position.z));
            }
        }

        private void OnValidate()
        {
            player = FindObjectOfType<PlayerInput>().transform;
        }
        
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