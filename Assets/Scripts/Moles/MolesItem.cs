using System;
using UnityEngine;

namespace Moles
{
    public class MolesItem : MonoBehaviour
    {
        private void OnDisable()
        {
            transform.parent.GetComponent<MoleItemsPool>().ReturnToPool(this);
        }
    }
}