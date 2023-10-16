using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    public class BricksSpawner : MonoBehaviour
    {
        [SerializeField] private BrickSpawnerMusicPattern pattern;
        [SerializeField] private List<GameObject> referenceBricks;
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

            yield return new WaitForSeconds(7);
            FindObjectOfType<EndScreenScript>().GameEnd();
        }

        private void SpawnBrick(BrickSpawningParams _params) =>
            Instantiate(referenceBricks[_params.numberOfTheTrack], spawningPoints[_params.numberOfTheTrack])
                .GetComponent<Brick>().Initialize(_params.brickParameters);
    }
}