using System;
using System.Collections;
using UnityEngine;

namespace Moles
{
    public class MolesItem : MonoBehaviour
    {
        [SerializeField] private float force = 10f;
        [SerializeField] [HideInInspector] internal Rigidbody m_rigidbody;
        [SerializeField] private MoleItemsPool m_MoleItemsPool;
        [SerializeField] private float lifetime = 8f;
        private int callid;

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
            StopAllCoroutines();
            if (m_MoleItemsPool == null) m_MoleItemsPool = FindObjectOfType<MoleItemsPool>();
            m_rigidbody.isKinematic = true;
            transform.localScale = startScale;
            StartCoroutine(LifeTimeEnd());
        }

        private IEnumerator LifeTimeEnd()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        public void Disable()
        {
            m_MoleItemsPool.ReturnToPool(this);
        }
    }
}