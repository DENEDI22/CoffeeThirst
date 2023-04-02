using UnityEngine;

namespace Moles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float startForce;
        [SerializeField] private float rotationRandomForce = 5;
        
        [SerializeField] [HideInInspector] private Rigidbody projectileRigidbody;

        private void OnValidate()
        {
            projectileRigidbody = GetComponent<Rigidbody>();
        }

        public void AddLaunchImpulse()
        {
            projectileRigidbody.isKinematic = false;
            transform.SetParent(transform.parent.parent);
            projectileRigidbody.AddForce(transform.forward * startForce, ForceMode.Impulse);
            projectileRigidbody.AddTorque(Random.onUnitSphere * rotationRandomForce);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Mole _collidedMole))
            {
                _collidedMole.Die();
            }

            Destroy(gameObject);
        }
    }
}