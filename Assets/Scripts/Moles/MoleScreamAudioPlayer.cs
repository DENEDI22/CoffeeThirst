using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moles
{
    [RequireComponent(typeof(AudioSource))]
    public class MoleScreamAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] MoleScreamVariants;
        private AudioSource m_audioSource;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        public void MoleDied()
        {
            m_audioSource.PlayOneShot(MoleScreamVariants[Random.Range(0, MoleScreamVariants.Length)]);
        }
    }
}