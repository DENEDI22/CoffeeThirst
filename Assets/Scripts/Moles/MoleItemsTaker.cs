using System;
using UnityEngine;

namespace Moles
{
    public class MoleItemsTaker : MonoBehaviour
    {
        public int score = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MolesItem molesItem))
            {
                score++;
                Destroy(other.gameObject);
            }
        }
    }
}