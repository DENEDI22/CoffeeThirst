using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chrobaki
{
    public class ChrobakyTimer : MonoBehaviour
    {
        [SerializeField] private Animator matkaAnimator, matkaUpDown;
        [SerializeField] private BugSpawner bugSpawner;
        [SerializeField] private Transform matkaOffset;
        [SerializeField] private BugMother bugMother;
        [SerializeField] private float matkaUpAnimDuration, matkaBugsThrowDelay, matkaUpTime;
        [SerializeField] private List<Transform> matkaPositions;
        private static readonly int IsUp = Animator.StringToHash("isUp");
        private static readonly int SpawnBugs = Animator.StringToHash("spawnBugs");
        [SerializeField] private Rigidbody playerRB;
        [SerializeField] private ParticleSystem matkaDiggingParticles;
        [SerializeField] private TextMeshProUGUI taskText;
        [SerializeField] private GameObject matkaHealthBar;
        [SerializeField] private CinemachineImpulseSource matkaShakeSource;
        [SerializeField] private CinemachineVirtualCamera matkaCam;

        public int waveCounter = 0;
        public bool isFirstWave => waveCounter == 0;

        public void UpdateTask()
        {
            if (isFirstWave)
                taskText.text = $"Залишилось вбити: \n {bugSpawner.bugsAlive} жуків";
            else
            {
                taskText.text = $"Залишилось вбити: \n {bugSpawner.bugsAlive} жуків та матку!";
                matkaHealthBar.SetActive(true);
            }
        }

        private IEnumerator MatkaChangePosition()
        {
            UpdateTask();
            matkaOffset.position = matkaPositions[Random.Range(0, matkaPositions.Count)].position;
            yield return new WaitForSeconds(2);
            matkaDiggingParticles.Play();
            matkaUpDown.SetBool(IsUp, true);
            if (waveCounter <= 1) matkaCam.Priority = 11;
            matkaShakeSource.GenerateImpulseWithForce(2);
            bugMother.isUp = true;
            bugMother.playerMarker.gameObject.SetActive(true);
            yield return new WaitForSeconds(matkaUpAnimDuration);
            matkaDiggingParticles.Stop();
            matkaCam.Priority = 9;
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