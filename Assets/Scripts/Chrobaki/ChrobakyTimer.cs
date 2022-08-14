using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chrobaki
{
    public class ChrobakyTimer : MonoBehaviour
    {
        [SerializeField] private Animator matkaAnimator, matkaUpDown;
        [SerializeField] private BugSpawner bugSpawner;
        [SerializeField] private Transform matkaOffset;
        [SerializeField] private float matkaUpAnimDuration, matkaBugsThrowDelay, matkaUpTime;
        [SerializeField] private List<Transform> matkaPositions;
        private static readonly int IsUp = Animator.StringToHash("isUp");
        private static readonly int SpawnBugs = Animator.StringToHash("spawnBugs");

        private void Start()
        {
            StartCoroutine(MatkaChangePosition());
        }

        private IEnumerator MatkaChangePosition()
        {
            matkaOffset.position = matkaPositions[Random.Range(0, matkaPositions.Count)].position;
            yield return new WaitForSeconds(2);
            matkaUpDown.SetBool(IsUp, true);
            yield return new WaitForSeconds(matkaUpAnimDuration);
            bugSpawner.StartCoroutine("SpawnBugs");
            matkaAnimator.SetTrigger(SpawnBugs);
            yield return new WaitForSeconds(matkaUpTime);
            matkaAnimator.SetTrigger(SpawnBugs);
            yield return new WaitForSeconds(matkaBugsThrowDelay);
            matkaUpDown.SetBool(IsUp, false);
        }
    }
}