using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RythmGame
{
    public class SongSelector : MonoBehaviour
    {
        [SerializeField] private MusicAndPatternContainer[] MusicPatterns;
        [SerializeField] private TMP_Dropdown patternSelector;
        [SerializeField] private BricksSpawner bricksSpawner;
        [SerializeField] private AudioSource m_musicSource;
        [SerializeField] private List<DestroyZone> destroyZones;
        [SerializeField] private Animator conveyerLine;

        public void Awake()
        {
            LoadLevelList();
        }

        public void LoadLevelList()
        {
            patternSelector.ClearOptions();
            List<string> patternNames = new List<string>();
            foreach (var musicAndPatternContainer in MusicPatterns)
            {
                patternNames.Add(musicAndPatternContainer.name);
            }

            patternSelector.AddOptions(patternNames);
        }

        public void SetMusic()
        {
            var musicPattern = MusicPatterns[patternSelector.value];
            bricksSpawner.SetPattern(musicPattern.brickSpawnerMusicPattern);
            m_musicSource.clip = musicPattern.audioClip;
            destroyZones.ForEach(x => x.soundsOnBreak = musicPattern.drumSamples);
            conveyerLine.speed *= musicPattern.brickSpawnerMusicPattern.gamespeedMultiplyer;
            bricksSpawner.SetPlayingState(true);
            gameObject.SetActive(false);
        }
    }
}