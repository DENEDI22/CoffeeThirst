using System;
using UnityEngine;

namespace Moles
{
    public class MolesItem : MonoBehaviour
    {
        [SerializeField] private float force = 10f;
        [SerializeField] [HideInInspector] private Rigidbody m_rigidbody;
        [SerializeField] private MoleItemsPool m_findObjectOfType;
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
            if (m_findObjectOfType == null) m_findObjectOfType = FindObjectOfType<MoleItemsPool>();
            transform.localScale = startScale;
        }

        public void Disable()
        {
            m_rigidbody.isKinematic = true;
            m_findObjectOfType.ReturnToPool(this);
        }
    }
}