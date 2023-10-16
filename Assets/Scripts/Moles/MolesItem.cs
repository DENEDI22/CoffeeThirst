using System;
using UnityEngine;

namespace Moles
{
    public class MolesItem : MonoBehaviour
    {
        [SerializeField] private float force = 10f;
        [SerializeField] [HideInInspector] private Rigidbody m_rigidbody;
        [SerializeField] private MoleItemsPool m_MoleItemsPool;
        private Vector3 startScale = new Vector3(0.3f, 0.3f, 0.3f);
        private void OnValidate()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            // startScale = transform.lossyScale;
        }

        private void FixedUpdate()
        {
            m_rigidbody.AddForce(new Vector3(0, 0, -1) * force);
        }

        private void OnEnable()
        {
            if (m_MoleItemsPool == null) m_MoleItemsPool = FindObjectOfType<MoleItemsPool>();
            transform.localScale = startScale;
        }

        public void Disable()
        {
            m_MoleItemsPool.ReturnToPool(this);
            m_rigidbody.isKinematic = true;
        }
    }
}