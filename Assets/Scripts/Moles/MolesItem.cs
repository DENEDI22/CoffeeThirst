using System;
using UnityEngine;

namespace Moles
{
    public class MolesItem : MonoBehaviour
    {
        [SerializeField] private float force = 10f;
        [SerializeField] [HideInInspector] private Rigidbody m_rigidbody;
        [SerializeField] private MoleItemsPool m_findObjectOfType;

        private void OnValidate()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            m_rigidbody.AddForce(new Vector3(0, 0, -1) * force);
        }

        private void OnEnable()
        {
            if (m_findObjectOfType == null) m_findObjectOfType = FindObjectOfType<MoleItemsPool>();
        }

        public void Disable()
        {
            m_rigidbody.isKinematic = true;
            m_findObjectOfType.ReturnToPool(this);
        }
    }
}