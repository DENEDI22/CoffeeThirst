using System;
using UnityEngine;

namespace Moles
{
    public class AimPoint : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_aimLine;

        private void FixedUpdate()
        {
            if(m_aimLine.positionCount > 0)
                transform.position = new Vector3(m_aimLine.GetPosition(m_aimLine.positionCount - 1).x, 10,
                    m_aimLine.GetPosition(m_aimLine.positionCount - 1).z);
        }
    }
}