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
        [SerializeField] private Rigidbody playerRB;
        [SerializeField] private ParticleSystem matkaDiggingParticles;

        private IEnumerator MatkaChangePosition()
        {
            matkaOffset.position = matkaPositions[Random.Range(0, matkaPositions.Count)].position;
            yield return new WaitForSeconds(2);
            matkaDiggingParticles.Play();
            matkaUpDown.SetBool(IsUp, true);
            yield return new WaitForSeconds(matkaUpAnimDuration);
            matkaDiggingParticles.Stop();
            bugSpawner.StartCoroutine("SpawnBugs");
            playerRB.AddExplosionForce(100f, matkaOffset.position + Vector3.up * 2, 10f);
            matkaAnimator.SetTrigger(SpawnBugs);
            yield return new WaitForSeconds(matkaUpTime);
            matkaAnimator.SetTrigger(SpawnBugs);
            yield return new WaitForSeconds(matkaBugsThrowDelay);
            matkaUpDown.SetBool(IsUp, false);
        }
    }
}