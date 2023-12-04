using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame.Alternative
{
    public class TapDetector : MonoBehaviour
    {
        [SerializeField] private BrickSpawnerMusicPattern currentMusicPattern;

        [SerializeField] private List<bool> triggerButtonsEnabled = new List<bool>();

        public void OnButtonPressed(int buttonIndex)
        {
            if (triggerButtonsEnabled[buttonIndex])
            {
            }
        }

        private void ActivateButton(int _buttonIndex)
        {
            triggerButtonsEnabled[_buttonIndex] = true;
            StartCoroutine(WaitForMiss(_buttonIndex));
        }

        private IEnumerator WaitForMiss(int _buttonIndex)
        {
            yield return new WaitForSeconds(0.3f);
            if (triggerButtonsEnabled[_buttonIndex])
            {
                Debug.Log($"Missed on button {_buttonIndex}");
                triggerButtonsEnabled[_buttonIndex] = false;
            }
        }

        private IEnumerator PlayMusicPattern()
        {
            yield return new WaitForSeconds(currentMusicPattern.startDelay); //waiting for the first block
            foreach (var param in currentMusicPattern.brickSpawningParams) //playing the sequence
            {
                triggerButtonsEnabled[param.numberOfTheTrack] = true;
            }
        }


        public void HitSuccess()
        {
            Debug.Log("Button hit in time");
        }
    }
}