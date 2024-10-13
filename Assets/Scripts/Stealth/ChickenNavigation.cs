using System;
using System.Collections;
using Stealth;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public enum ChickenStates
{
    Patroling,
    Observing,
    Chase
}

public class ChickenNavigation : MonoBehaviour
{
    [SerializeField] private ChickenStates currentChickenState;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float timeToDetect;
    [SerializeField] private Image detectionTimerProgressBar;


    [SerializeField] private Transform headCheckingFOVPoint;
    [SerializeField] public float radius;
    [Range(0, 360)] [SerializeField] public float angle;

    [SerializeField] public GameObject playerRef;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        currentChickenState = ChickenStates.Patroling;
        while (!canSeePlayer)
        {
            while (true)
            {
                var i = Random.Range(0, patrolPoints.Length);
                var item = patrolPoints[i];
                if (canSeePlayer) break;
                GetComponent<NavMeshAgent>().SetDestination(item.position);
                yield return new WaitUntil(() => Vector3.Distance(transform.position, item.position) < 0.5f);
            }
        }
    }

    public void GoToPlayersSpot(Vector3 _playerPosition)
    {
        if (currentChickenState == ChickenStates.Patroling)
        {
            StopAllCoroutines();
            StartCoroutine("GoToPlayerSpotCor", _playerPosition);
        }
    }

    private IEnumerator GoToPlayerSpotCor(Vector3 _playerPosition)
    {
        GetComponent<NavMeshAgent>().SetDestination(_playerPosition);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, _playerPosition) <= 1.5f);
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        // FindObjectOfType<ChickenDetectionInformation>().OnPlayerDetected();
        currentChickenState = ChickenStates.Chase;
        while (canSeePlayer)
        {
            GetComponent<NavMeshAgent>().SetDestination(playerRef.transform.position);
            yield return new WaitForSeconds(0.5f);
            if (Vector3.Distance(transform.position, playerRef.transform.position) <= 1.8f)
            {
                playerRef.GetComponent<PlayerHealth>().TakeDamage(1);
                //TODO add animation
            }

            if (playerRef.GetComponent<PlayerSafeZoneChecker>().PlayerIsInSafeZone)
            {
                canSeePlayer = false;
                StartCoroutine("Patrol");
                yield break;
            }

            OnPlayerLost();
            yield break;
        }
    }

    private IEnumerator StartDetectingPlayer()
        {
            for (float i = 0; i < timeToDetect; i += 0.01f)
            {
                detectionTimerProgressBar.fillAmount = i / timeToDetect;
                GetComponent<NavMeshAgent>().isStopped = true;
                yield return new WaitForSeconds(0.01f);
                if (canSeePlayer == false)
                {
                    GetComponent<NavMeshAgent>().isStopped = false;
                    detectionTimerProgressBar.fillAmount = 0;
                    StartCoroutine(Patrol());
                    yield break;
                }
            }

            StartCoroutine(FollowPlayer());
        }

        // private IEnumerator FOVRoutine()
        // {
        //     WaitForSeconds wait = new WaitForSeconds(0.2f);
        //
        //     while (true)
        //     {
        //         yield return wait;
        //         FieldOfViewCheck(transform);
        //         if (!canSeePlayer) FieldOfViewCheck(headCheckingFOVPoint);
        //     }
        // }

        private IEnumerator CheckPlayersLastPosition(Vector3 _lastKnownPosition)
        {
            currentChickenState = ChickenStates.Patroling;
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(_lastKnownPosition);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, navMeshAgent.destination) < 0.5f);
            yield return new WaitForSeconds(2f);
            currentChickenState = ChickenStates.Patroling;
            StartCoroutine(Patrol());
        }

        private void OnPlayerDetected()
        {
            if (canSeePlayer == false)
            {
                StartCoroutine("StartDetectingPlayer");
            }
            else if(currentChickenState != ChickenStates.Chase)
            {
                transform.LookAt(playerRef.transform);
            }
        }

        private void OnPlayerLost()
        {
            StartCoroutine(CheckPlayersLastPosition(playerRef.transform.position));
            detectionTimerProgressBar.fillAmount = 0;
        }

        private void FixedUpdate()
        { 
            FieldOfViewCheck(transform); 
            if (!canSeePlayer) FieldOfViewCheck(headCheckingFOVPoint);
        }

        private void FieldOfViewCheck(Transform _fovPoint)
        {
            Collider[] rangeChecks = Physics.OverlapSphere(_fovPoint.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - _fovPoint.position).normalized;

                if (Vector3.Angle(_fovPoint.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(_fovPoint.position, target.position);

                    if (!Physics.Raycast(_fovPoint.position, directionToTarget, distanceToTarget, obstructionMask) && !playerRef.GetComponent<PlayerSafeZoneChecker>().PlayerIsInSafeZone)
                    {
                                OnPlayerDetected();
                                canSeePlayer = true;
                    }
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;
        }
    }