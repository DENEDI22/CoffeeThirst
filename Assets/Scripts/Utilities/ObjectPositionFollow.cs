using System;
using UnityEngine;

namespace Utilities
{
    public class ObjectPositionFollow : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private Vector3 offset;
        
        private void FixedUpdate()
        {
            transform.position = objectToFollow.position + offset;
        }
    }
}