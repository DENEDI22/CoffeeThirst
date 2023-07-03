using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDrawer : MonoBehaviour
{
    [SerializeField] [HideInInspector] private LineRenderer ropeRenderer;
    [SerializeField] private Transform anchorPoint, movingPoint;
    
    private void OnValidate()
    {
        ropeRenderer = GetComponent<LineRenderer>();
        ropeRenderer.SetPosition(0, anchorPoint.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        ropeRenderer.SetPosition(1, movingPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
