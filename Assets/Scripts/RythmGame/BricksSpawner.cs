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
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private EndScreenScript endScreenScript;
        
        private bool isPlaying, isSpawningBricksBeforePlaying = false;
        private int currentBrickIndex = 0;

        public void SetPattern(BrickSpawnerMusicPattern _pattern) => pattern = _pattern;

        public void SetPlayingState(bool _newState)
        {
            isPlaying = _newState;
            if (pattern.brickSpawningParams[currentBrickIndex].nextParamTime > 0)
            {
                musicSource.Play();
            }
            Invoke("GameEnd", musicSource.clip.length+1f);
        }

        public void FixedUpdate()
        {
            if (isPlaying && !isSpawningBricksBeforePlaying)
            {
                if (pattern.brickSpawningParams[currentBrickIndex].nextParamTime < 0)
                {
                    StartCoroutine(SpawnBricksBeforePlaying());
                }

                if (musicSource.time >= pattern.brickSpawningParams[currentBrickIndex].nextParamTime - 0.04 &&
                    musicSource.time <= pattern.brickSpawningParams[currentBrickIndex].nextParamTime + 0.04)
                {
                    SpawnBrick(pattern.brickSpawningParams[currentBrickIndex]);
                    currentBrickIndex++;
                }
                else if (musicSource.time >= pattern.brickSpawningParams[currentBrickIndex].nextParamTime + 0.3f)
                {
                    currentBrickIndex++;
                }
            }
        }

        private IEnumerator SpawnBricksBeforePlaying()
        {
            isSpawningBricksBeforePlaying = true;
            musicSource.PlayScheduled((-pattern.brickSpawningParams[currentBrickIndex].nextParamTime) + 2);
            while (pattern.brickSpawningParams[currentBrickIndex].nextParamTime < 0)
            {
                SpawnBrick(pattern.brickSpawningParams[currentBrickIndex]);
                currentBrickIndex++;
                if (pattern.brickSpawningParams[currentBrickIndex].nextParamTime < 0)
                {
                    yield return new WaitForSeconds(pattern.brickSpawningParams[currentBrickIndex - 1].nextParamTime -
                                                    pattern.brickSpawningParams[currentBrickIndex].nextParamTime);
                }
                else
                {
                    break;
                }
            }

            isSpawningBricksBeforePlaying = false;
            yield break;
        }


        private IEnumerator ExecutePattern()
        {
            yield return new WaitForSecondsRealtime(pattern.startDelay);
            foreach (var item in pattern.brickSpawningParams)
            {
                SpawnBrick(item);
                yield return new WaitForSecondsRealtime(item.nextParamTime);
            }

            yield return new WaitForSeconds(7);
            GameEnd();
        }

        private void GameEnd()
        {
            endScreenScript.GameEnd();
        }

        private void SpawnBrick(BrickSpawningParams _params) =>
            Instantiate(referenceBricks[_params.numberOfTheTrack], spawningPoints[_params.numberOfTheTrack])
                .GetComponent<Brick>().Initialize(_params.brickParameters);
    }
}