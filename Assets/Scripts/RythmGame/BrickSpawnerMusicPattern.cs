using System;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    [CreateAssetMenu(fileName = "NewBrickPattern", menuName = "RythmGame/Brick pattern", order = 0)]
    public class BrickSpawnerMusicPattern : ScriptableObject
    {
        public float startDelay;
        public List<BrickSpawningParams> brickSpawningParams;
    }
    
    [Serializable]
    public class BrickSpawningParams
    {
        public BrickParameters brickParameters;
        public float nextParamDelay;
        public int numberOfTheTrack;
    }
}