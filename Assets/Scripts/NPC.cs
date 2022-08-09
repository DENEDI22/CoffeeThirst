using Dialogues;
using UnityEngine;

namespace DefaultNamespace
{
    public class NPC : MonoBehaviour, IInteractable
    {
        private IInteractable _interactableImplementation;
        [SerializeField] private GameObject hint;
        [SerializeField] [HideInInspector] private DialogueShower dialogueShower;
        // [SerializeField] public Animator NPCPhrase;
        [SerializeField] public DialogueSO nextDialogue;

        private void OnValidate()
        {
            dialogueShower = FindObjectOfType<DialogueShower>();
        }

        public bool OnInteract()
        {
            dialogueShower.StartDialogue(nextDialogue);
            return true;
        }

        public void Select()
        {
            hint.SetActive(true);
            
        }

        public void Deselect()
        {
            hint.SetActive(false);
        }
    }
}