﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chrobaki
{
    public class BugSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bug;
        [SerializeField] private int amountOfBugsToThrow;
        [SerializeField] private BushArray bushArray;
        [SerializeField] public int bugsAlive { get; private set; }

        private void OnValidate()
        {
            bushArray = FindObjectOfType<BushArray>();
        }

        public void BugDeath()
        {
            ChrobakyTimer chrobakyTimer = FindObjectOfType<ChrobakyTimer>();
            bugsAlive--;
            chrobakyTimer.UpdateTask();
            if (bugsAlive <= 0)
            {
                chrobakyTimer.StartCoroutine("MatkaChangePosition");
                chrobakyTimer.waveCounter = 0;
            }
        }

        public void AddBugManually()
        {
            bugsAlive++;
        }

        private IEnumerator SpawnBugs()
        {
            List<Bush> bushesToThrowBugsAt = new List<Bush>();
            for (int i = 0; i < amountOfBugsToThrow; i++)
            {
                try
                {
                    bushesToThrowBugsAt.Add(bushArray.bushes[Random.Range(0, bushArray.bushes.Count)]);
                }
                catch (Exception e)
                {
                    //ignore
                }
            }

            foreach (Bush bush in bushesToThrowBugsAt)
            {
                bugsAlive++;
                var currentBug = Instantiate(bug, transform.position, Quaternion.identity).GetComponent<Bug>();
                currentBug.startingPoint = transform.position;
                var bugPosition = bush.GetBugPosition();
                currentBug.endingPoint = bugPosition.position;
                currentBug.transform.rotation = bugPosition.rotation;
                currentBug.flyingInTheAir = true;
                currentBug.StartCoroutine("Fly");
                bush.hasBugOnIt = true;
                bush.bugsOnTheBush.Add(currentBug);
                bush.playerMarker.gameObject.SetActive(true);
                bush.StartCoroutine("ReduceHealth");
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}