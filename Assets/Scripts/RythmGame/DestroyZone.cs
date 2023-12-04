using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RythmGame
{
    public class DestroyZone : MonoBehaviour
    {
        private Queue<Brick> brickInZone = new Queue<Brick>();
        public List<Brick> brickInSafeZone = new List<Brick>();
        public List<Brick> brickInPerfectZone = new List<Brick>();
        public AudioClip MissclickAudioClip;
        public AudioSource soundsAudioSource;
        [SerializeField] private Animator bladeAnimator;
        [SerializeField] private float bladeAnimationDuration;
        public List<AudioClip> soundsOnBreak;
        [HideInInspector] [SerializeField] private ScoreCounter m_scoreCounter;

        private void OnValidate()
        {
            m_scoreCounter = FindObjectOfType<ScoreCounter>();
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.TryGetComponent(out Brick brick))
            {
                if (!brickInZone.Contains(brick)) brickInZone.Enqueue(brick);
            }
        }

        public void BreakButtonPress(InputAction.CallbackContext _ctx)
        {
            if (_ctx.performed)
            {
                if (brickInZone.Count > 0) StartCoroutine(Slash(brickInZone.Dequeue()));
                else
                {
                    m_scoreCounter.AddScore(-300);
                    MissClick();
                }
            }
        }

        private IEnumerator Slash(Brick _brickData)
        {
            bladeAnimator.SetTrigger("Hit");
            if (_brickData && !_brickData.isBroken)
            {
                soundsAudioSource.PlayOneShot(_brickData
                    ? soundsOnBreak[(int)_brickData.brickParameters.soundType]
                    : MissclickAudioClip);
                var tmp = _brickData;
                if (brickInPerfectZone.Contains(_brickData))
                {
                    brickInPerfectZone.Remove(_brickData);
                    brickInSafeZone.Remove(_brickData);
                    yield return new WaitForSeconds(0.08f);
                    m_scoreCounter.AddScore(500);

                }
                else if (brickInSafeZone.Contains(_brickData))
                {
                    brickInSafeZone.Remove(_brickData);
                    yield return new WaitForSeconds(0.08f);
                    m_scoreCounter.AddScore(250);
                }
                else
                {
                    yield return new WaitForSeconds(0.08f);
                    m_scoreCounter.AddScore(100);
                }
                tmp.Break();
            }
        }

        public void MissClick()
        {
            bladeAnimator.SetTrigger("Hit");
            soundsAudioSource.PlayOneShot(MissclickAudioClip);
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Brick brick) && !brick.isBroken)
            {
                brickInZone.Dequeue();
            }
        }
    }
}