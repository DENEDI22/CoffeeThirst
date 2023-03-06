using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RythmGame
{
    public class DestroyZone : MonoBehaviour
    {
        public Brick brickInZone;
        public AudioClip MissclickAudioClip;
        public AudioSource soundsAudioSource;

        public List<AudioClip> soundsOnBreak;
        public void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out Brick brick))
            {
                brickInZone = brick;
            }
        }

        public void BreakButtonPress(InputAction.CallbackContext _ctx)
        {
            if (_ctx.performed)
            {
                if (brickInZone != null)
                {
                    soundsAudioSource.PlayOneShot(soundsOnBreak[(int)brickInZone.brickParameters.soundType]);
                    brickInZone.Break();
                }
            }
        }

        public void MissClick()
        {
            soundsAudioSource.PlayOneShot(MissclickAudioClip);
        }
        
        public void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out Brick brick) && brick == brickInZone)
            {
                brickInZone.Miss();
            }
        }
    }
}