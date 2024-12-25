using System;
using Cinemachine;
using UnityEngine;

public class SensitivitySettings : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook flCamera;
    [SerializeField] private float defaultX, defaultY;

    public void SetMultiplier(Single _value)
    {
        flCamera.m_XAxis.m_MaxSpeed = defaultX * _value;
        flCamera.m_YAxis.m_MaxSpeed = defaultY * _value;
    }
}
