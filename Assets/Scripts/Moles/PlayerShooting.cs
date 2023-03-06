using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Moles
{
    public class PlayerShooting : MonoBehaviour
    {
        public List<Projectile> projectilePrefabs;
        public Transform projectileSpawnPoint;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private float startProjectileSpeed;
        [SerializeField] private float cooldownTime = 0.7f;
        [SerializeField] public LineRenderer trajectoryRenderer;

        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;
        public float lookYLimit = 45.0f;
        float m_rotationX, m_rotationY = 0;
        private Projectile currentProjectile;
        private float m_forceOfProjectileImpulse;
        private bool m_isOnCooldown = false;

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
            m_rotationY = Mathf.Clamp(m_rotationY, -lookYLimit, 10);
        }

        public void Shoot(InputAction.CallbackContext _ctx)
        {
            if (_ctx.performed && !m_isOnCooldown)
            {
                currentProjectile.AddLaunchImpulse();
                currentProjectile = Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Count)],
                    projectileSpawnPoint);
                m_isOnCooldown = true;
                Invoke("Cooldown", cooldownTime);
            }
        }

        private void Cooldown() => m_isOnCooldown = false;

        private void FixedUpdate()
        {
            projectileSpawnPoint.eulerAngles = new Vector3(m_rotationY, m_rotationX, 0);
            if (!m_isOnCooldown) ShowTrajectory(projectileSpawnPoint.position, ShootDirection());
            else trajectoryRenderer.positionCount = 0;
        }

        public Vector3 ShootDirection() => projectileSpawnPoint.transform.forward * startProjectileSpeed;

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            Vector3[] points = new Vector3[10];
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