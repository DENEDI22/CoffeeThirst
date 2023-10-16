using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace RythmGame
{
    public class brickZones : MonoBehaviour
    {
        [SerializeField] private bool safeZone = true;
        [SerializeField] private bool perfectZone = false;
        [SerializeField] private DestroyZone parentZone;
        
        private void OnValidate()
        {
            parentZone = GetComponentInParent<DestroyZone>();
            if (safeZone == perfectZone)
            {
                Debug.LogError("Zone Can't be safe and Perfect at the same time");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out Brick _brick))
            {
                if (safeZone)
                {
                    if(!parentZone.brickInSafeZone.Contains(_brick)) parentZone.brickInSafeZone.Add(_brick);
                }
                else if(perfectZone)
                {
                    if(!parentZone.brickInPerfectZone.Contains(_brick)) parentZone.brickInPerfectZone.Add(_brick);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out Brick _brick))
            {
                if (safeZone)
                {
                    parentZone.brickInSafeZone.Remove(_brick);
                }
                else if(perfectZone)
                {
                    parentZone.brickInPerfectZone.Remove(_brick);
                }
            }
        }
    }
}