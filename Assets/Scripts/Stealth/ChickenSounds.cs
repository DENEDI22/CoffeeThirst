using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stealth
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ChickenNavigation))]
    public class ChickenSounds : MonoBehaviour
    {
        [SerializeField] private float minimalDelay;
        [SerializeField] private float maximalDelay;
        [Space] [SerializeField] private List<AudioClip> idleSounds;
        [SerializeField] private List<AudioClip> agressiveSounds;
        [Space] [SerializeField] private ChickenNavigation chickenNavigation;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            chickenNavigation = GetComponent<ChickenNavigation>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(PlaySound());
        }

        private IEnumerator PlaySound()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minimalDelay, maximalDelay));
                if (chickenNavigation.GetCurrentState == ChickenStates.Patroling)
                {
                    audioSource.panStereo = 1;
                    audioSource.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Count)]);
                }
                else
                {
                    audioSource.panStereo = 0.6f;
                    audioSource.PlayOneShot(agressiveSounds[Random.Range(0, agressiveSounds.Count)]);
                }
            }
        }
    }
}