using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private int duration;
        [SerializeField] private UnityEvent onTimerStarted, onTimerFinished, onTimerTerminated;
        [SerializeField] private TextMeshProUGUI timerDisplay;
        private bool isTimerActive = false;
        private void Start()
        {
            StartCoroutine(TimerCorutine());
            isTimerActive = true;
        }
        private IEnumerator TimerCorutine()
        {
            for (int i = duration; i >= 0; i--)
            {
                timerDisplay.text = $"{i/60}:{i%60}";
                yield return new WaitForSeconds(1);
            }
            onTimerFinished.Invoke();
        }
        /// <summary>
        /// Starts a timer changing it's duration at first
        /// </summary>
        /// <param name="_duration">New duration</param>
        public void StartTimer(int _duration)
        {
            duration = _duration;
            StartTimer();
        }
        /// <summary>
        /// Starts a timer without changing it's duration
        /// </summary>
        public void StartTimer()
        {
            if (isTimerActive)
            {
                Debug.LogAssertion("Timer has already been started. Terminate timer before starting it over.", this);
                return;
            }
            StartCoroutine(TimerCorutine());
            isTimerActive = true;
            onTimerStarted.Invoke();
        }
        
        public void TerminateTimer()
        {
            StopCoroutine(TimerCorutine());
            isTimerActive = false;
            onTimerTerminated.Invoke();
        }
    }
}