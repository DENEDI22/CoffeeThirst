using System;
using UnityEngine;

namespace RythmGame
{
    public class MissedBrickTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreCounter scoreCounter;
        [SerializeField] private AudioSource missedBrickSoundSource;
        [SerializeField] private AudioClip missedBrickSoundClip;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Brick brick) && !brick.isBroken)
            {
                scoreCounter.AddScore(-600);
                missedBrickSoundSource.PlayOneShot(missedBrickSoundClip);
            }
        }
    }
}