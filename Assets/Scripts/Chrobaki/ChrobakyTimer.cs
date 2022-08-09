using System;
using System.Collections;
using UnityEngine;

namespace Chrobaki
{
    public class ChrobakyTimer : MonoBehaviour
    {
        [SerializeField] private Animator matkaAnimator, matkaUpDown;
        [SerializeField] private BugSpawner bugSpawner;

        [SerializeField] private float matkaUpAnimDuration, matkaBugsThrowDelay, matkaUpTime, matkaRecharge;

        private static readonly int IsUp = Animator.StringToHash("isUp");
        private static readonly int SpawnBugs = Animator.StringToHash("spawnBugs");

        private void Start()
        {
            StartCoroutine(MatkaChangePosition());
        }

        private IEnumerator MatkaChangePosition()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                matkaUpDown.SetBool(IsUp, true);
                yield return new WaitForSeconds(matkaUpAnimDuration);
                bugSpawner.StartCoroutine("SpawnBugs"); 
                matkaAnimator.SetTrigger(SpawnBugs);
                yield return new WaitForSeconds(matkaUpTime);
                matkaAnimator.SetTrigger(SpawnBugs);
                yield return new WaitForSeconds(matkaBugsThrowDelay);
                matkaUpDown.SetBool(IsUp, false);
                yield return new WaitForSeconds(matkaRecharge);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}