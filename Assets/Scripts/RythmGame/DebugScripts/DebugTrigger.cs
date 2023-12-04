using System;
using UnityEngine;

namespace RythmGame.DebugScripts
{
    public class DebugTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Brick brick) && !brick.isBroken)
            {
                Debug.Log("WeirdShit");
            }
        }
    }
}