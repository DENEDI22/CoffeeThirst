using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RythmGame
{
    [CreateAssetMenu(fileName = "NewBrickPattern", menuName = "RythmGame/Brick pattern", order = 0)]
    public class BrickSpawnerMusicPattern : ScriptableObject
    {
        public float startDelay;
        public List<BrickSpawningParams> brickSpawningParams;
        public float gamespeedMultiplyer = 1;
    }
    
    [Serializable]
    public class BrickSpawningParams
    {
        public BrickParameters brickParameters;
        [FormerlySerializedAs("nextParamDelay")] public float nextParamTime;
        public int numberOfTheTrack;
    }
}