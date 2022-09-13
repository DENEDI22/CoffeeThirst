using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Moles
{
    public class PlayerShooting : MonoBehaviour
    {
        public List<Projectile> projectilePrefabs;
        public Transform projectileSpawnPoint;
        [SerializeField] private Animator playerAnimator;
        
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f; 
        float rotationX = 0;
        
        private float forceOfProjectileImpulse;
        public void Aim(InputAction.CallbackContext _ctx)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        }

        public void Shoot(InputAction.CallbackContext _ctx)
        {
            var _proj = Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)]);
            
        }

        private void Update()
        {
            projectileSpawnPoint.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}