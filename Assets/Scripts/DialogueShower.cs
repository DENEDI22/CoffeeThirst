using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialogues
{
    public class DialogueShower : MonoBehaviour
    {
        [SerializeField] private GameObject phraseFrame;
        [SerializeField] [Range(2, 50)] private float textShowingSpeed;
        [SerializeField] [HideInInspector] private TextMeshProUGUI phraseTextMeshPro;
        [SerializeField] private AudioSource hummingSoundSource;
        [SerializeField] [HideInInspector] private PlayerInput playerInput;
        private DialogueSO currentDialogue;
        private int currentPhraseIndex, currentLastPhraseIndex;
        private List<Coroutine> textShowers = new List<Coroutine>();

        private void OnValidate()
        {
            if (phraseFrame != null) phraseTextMeshPro = phraseFrame.GetComponentInChildren<TextMeshProUGUI>();
            playerInput = FindObjectOfType<PlayerInput>();
        }

        private void ShowNextPhrase()
        {
            phraseTextMeshPro.text = string.Empty;
            currentPhraseIndex++;
            StopAllCoroutines();
            StartCoroutine(ShowText());
        }

        private IEnumerator ShowText()
        {
            if (currentPhraseIndex > currentLastPhraseIndex)
            {
                FinishDialogue();
                yield break;
            }
            // if (currentDialogue.dialoguePhrases[currentPhraseIndex].person.hummingSounds.Count < 1)
            // {
            //     hummingSoundSource.clip = currentDialogue.dialoguePhrases[currentPhraseIndex].person
            //         .hummingSounds[
            //             Random.Range(0,
            //                 currentDialogue.dialoguePhrases[currentPhraseIndex].person.hummingSounds.Count)];
            //     hummingSoundSource.Play();
            // }
            foreach (var nextChar in currentDialogue.dialoguePhrases[currentPhraseIndex].phraseText.ToCharArray())
            {
                phraseTextMeshPro.text += nextChar.ToString();
                yield return new WaitForSecondsRealtime(1 / textShowingSpeed);
            }
        }

        public void PlayerPhraseChange(InputAction.CallbackContext _ctx)
        {
            if (_ctx.performed)
            {
                ShowNextPhrase();
            }
        }

        public void StartDialogue(DialogueSO _dialogueSo)
        {
            phraseFrame.SetActive(true);
            currentDialogue = _dialogueSo;
            currentLastPhraseIndex = currentDialogue.dialoguePhrases.Count - 1;
            currentPhraseIndex = -1;
            playerInput.SwitchCurrentActionMap("Dialogues");
            ShowNextPhrase();
        }

        private void FinishDialogue()
        {
            currentDialogue = null;
            playerInput.SwitchCurrentActionMap("DefaultMap");
            phraseFrame.SetActive(false);
        }
    }
}