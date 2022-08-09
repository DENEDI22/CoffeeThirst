using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [Serializable]
    public class DialoguePhrase
    {
        [SerializeField] public string phraseText;
        [SerializeField] public DialoguePersonSettings person;
    }
    
    [Serializable]
    public class DialoguePersonSettings
    {
        [SerializeField] public List<AudioClip> hummingSounds;
    }
    
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField] public List<DialoguePhrase> dialoguePhrases;
    }
}