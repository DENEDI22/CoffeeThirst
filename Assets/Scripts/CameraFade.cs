using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    [SerializeField] private float maximalDistance = 3.5f;
    [SerializeField] private AnimationCurve fadingCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private Renderer m_renderer;
    [SerializeField] private Material fadeMaterial, opaqueMaterial;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, activeCamera.transform.position) < maximalDistance)
        {
            Color materialColor = m_renderer.material.color;
            if(m_renderer.material != fadeMaterial) m_renderer.material = fadeMaterial;
            m_renderer.material.SetColor("_Color", new Color(r: materialColor.r, g: materialColor.g,
                b: materialColor.b,
                fadingCurve.Evaluate(Vector3.Distance(transform.position, activeCamera.transform.position) /
                                     maximalDistance)));
        }
        else if(m_renderer.material != opaqueMaterial)
        {
            m_renderer.material = opaqueMaterial;
        }
    }
}