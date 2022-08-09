using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class HouseRoofRemover : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPlayerIn;
        [SerializeField] private UnityEvent onPlayerOut;
        [SerializeField] private MeshRenderer roof;
        [SerializeField] private bool autoLocateRoof;
        private static readonly int Color1 = Shader.PropertyToID("_Color");


        private void OnValidate()
        {
            if (autoLocateRoof)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.layer == 3)
                    {
                        roof = child.GetComponent<MeshRenderer>();
                        break;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInput input))
            {
                onPlayerIn.Invoke();
                roof.material.SetColor(Color1, new Color(roof.material.color.r, roof.material.color.g, roof.material.color.b, 0.3f));
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerInput input))
            {
                onPlayerOut.Invoke();
                roof.material.SetColor(Color1, new Color(roof.material.color.r, roof.material.color.g, roof.material.color.b, 1));
            }
        }
    }
}