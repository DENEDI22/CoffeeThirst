using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    [CreateAssetMenu(fileName = "newMusicAndPattern", menuName = "RythmGame/Music and Pattern container", order = 0)]
    public class MusicAndPatternContainer : ScriptableObject
    {
        [SerializeField] public BrickSpawnerMusicPattern brickSpawnerMusicPattern;
        [SerializeField] public AudioClip audioClip;
        [SerializeField] public List<AudioClip> drumSamples; 
    }
}