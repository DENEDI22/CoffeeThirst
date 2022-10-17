using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moles
{
    public class MoleArray : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private List<Mole> moles = new List<Mole>();
        [SerializeField] private float minDelay, maxDelay;
        [SerializeField] private float minMoleStayTime, maxMoleStayTime;
        [SerializeField] private int minMoleQuantity, maxMoleQuantity;


        private void OnValidate()
        {
            moles = new List<Mole>();
            moles.AddRange(FindObjectsOfType<Mole>());
        }

        private IEnumerator SpawnMole()
        {
            int moleQuantity = Random.Range(minMoleQuantity, maxMoleQuantity);
            for (int i = 0; i < moleQuantity; i++)
            {
                
            }
            yield break;
        }
    }
}