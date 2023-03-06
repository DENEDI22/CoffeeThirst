using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    public class BricksSpawner : MonoBehaviour
    {
        [SerializeField] private BrickSpawnerMusicPattern pattern;
        [SerializeField] private GameObject referenceBrick;
        [SerializeField] private List<Transform> spawningPoints;

        private void Start()
        {
            StartCoroutine(ExecutePattern());
        }

        private IEnumerator ExecutePattern()
        {
            foreach (var item in pattern.brickSpawningParams)
            {
                SpawnBrick(item);
                yield return new WaitForSeconds(item.nextParamDelay);
            }
            yield break;
        }

        private void SpawnBrick(BrickSpawningParams _params) => Instantiate(referenceBrick, spawningPoints[_params.numberOfTheTrack]).GetComponent<Brick>().Initialize(_params.brickParameters);
    }
}