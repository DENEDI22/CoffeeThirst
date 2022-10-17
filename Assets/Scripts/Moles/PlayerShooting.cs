using System;
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
        [SerializeField] private float startProjectileSpeed;

        [SerializeField] public LineRenderer trajectoryRenderer;

        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;
        public float lookYLimit = 45.0f;
        float m_rotationX, m_rotationY = 0;
        private Projectile currentProjectile;
        private float m_forceOfProjectileImpulse;

        private void Start()
        {
            currentProjectile = Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)],
                projectileSpawnPoint);
        }

        public void Aim(InputAction.CallbackContext _ctx)
        {
            m_rotationX += -_ctx.ReadValue<Vector2>().x * lookSpeed;
            m_rotationX = Mathf.Clamp(m_rotationX, -lookXLimit, lookXLimit);
            m_rotationY += _ctx.ReadValue<Vector2>().y * lookSpeed * 2;
            m_rotationY = Mathf.Clamp(m_rotationY, -lookYLimit, lookYLimit);
        }

        public void Shoot(InputAction.CallbackContext _ctx)
        {
            if (_ctx.performed)
            {
                currentProjectile.AddLaunchImpulse();
                currentProjectile = Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)],
                    projectileSpawnPoint);
            }
        }

        private void FixedUpdate()
        {
            // projectileSpawnPoint.localRotation = Quaternion.Euler(0, rotationX * 0.01f, 0);
            projectileSpawnPoint.eulerAngles = new Vector3(m_rotationY, m_rotationX, 0);
            ShowTrajectory(projectileSpawnPoint.position, ShootDirection());
        }

        public Vector3 ShootDirection() => projectileSpawnPoint.transform.forward * startProjectileSpeed;

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            Vector3[] points = new Vector3[100];
            trajectoryRenderer.positionCount = points.Length;

            for (int i = 0; i < points.Length; i++)
            {
                float time = i * 0.05f;
                points[i] = origin + speed * time + Physics.gravity * (time * time) / 2f;
            }

            trajectoryRenderer.SetPositions(points);
        }
    }
}