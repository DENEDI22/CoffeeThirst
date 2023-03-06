using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moles
{
    public class MoleArray : MonoBehaviour
    {
        [SerializeField] private List<Mole> freeMoles = new List<Mole>();
        [SerializeField] private float minDelay, maxDelay; //pause between moles spawning
        [SerializeField] private float minMoleStayTime, maxMoleStayTime; //time one mole stays above the ground
        [SerializeField] private int minMoleQuantity, maxMoleQuantity;

        private List<Mole> molesInUse = new List<Mole>();

        private void OnValidate()
        {
            freeMoles = new List<Mole>();
            freeMoles.AddRange(FindObjectsOfType<Mole>());
        }

        private void Start()
        {
            StartCoroutine(MoleCycle());
        }

        private IEnumerator MoleCycle()
        {
            while (true)
            {
                int moleQuantity = Random.Range(minMoleQuantity, maxMoleQuantity);
                for (int i = 0; i < moleQuantity; i++)
                {
                    SpawnMole();
                }
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            }
        }

        private void SpawnMole()
        {
            var mole = freeMoles[Random.Range(0, freeMoles.Count)];
            mole.Appear();
            mole.StartLifeCycle(Random.Range(minMoleStayTime, maxMoleStayTime));
            freeMoles.Remove(mole);
            molesInUse.Add(mole);
            mole.onMoleDisappear.AddListener(MoleToFreeArray);
        }

        private void MoleToFreeArray(Mole _mole)
        {
            molesInUse.Remove(_mole);
            freeMoles.Add(_mole);
        }
    }
}