using System;
using UnityEngine;

namespace Moles
{
    public class MoleObjectCollector : MonoBehaviour
    {
        [SerializeField] private WinLooseLogic m_winLooseLogic;
        
        private void OnValidate()
        {
            m_winLooseLogic = FindObjectOfType<WinLooseLogic>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out MolesItem molesItem))
            {
                molesItem.Disable();
                m_winLooseLogic.CatchMoleObject();
            }
        }
    }
}