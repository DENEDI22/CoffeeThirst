using UnityEngine;

namespace Stealth
{
    public class FOVCheck : MonoBehaviour
    {
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float fieldOfViewAngle = 45f;
        [SerializeField] private LayerMask obstacleMask;
        
        public bool Detect(Transform _transformToTrack)
        {
            Vector3 direction = (_transformToTrack.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, _transformToTrack.position);

            if (distance < detectionRange)
            {
                float angle = Vector3.Angle(transform.forward, direction);
                if (angle < fieldOfViewAngle / 2)
                {
                    if (!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}