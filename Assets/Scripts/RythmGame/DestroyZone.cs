using System;
using System.Collections;
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
        [SerializeField] private Animator bladeAnimator;
        [SerializeField] private float bladeAnimationDuration;

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
                StartCoroutine(Slash());
            }
        }

        private IEnumerator Slash()
        {
            soundsAudioSource.PlayOneShot(brickInZone
                ? soundsOnBreak[(int)brickInZone.brickParameters.soundType]
                : MissclickAudioClip);
            bladeAnimator.SetTrigger("Hit");
            if (brickInZone)
            {
                var tmp = brickInZone;
                yield return new WaitForSeconds(0.3f);
                tmp.Break();
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
            brickInZone = null;
        }
    }
}