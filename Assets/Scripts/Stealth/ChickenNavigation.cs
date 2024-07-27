using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [Range(0,360)]
    [SerializeField]
    public float angle;

    [SerializeField] public GameObject playerRef;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    [SerializeField] public bool canSeePlayer;
    
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
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

    private IEnumerator FollowPlayer()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        while (canSeePlayer)
        {
            GetComponent<NavMeshAgent>().SetDestination(playerRef.transform.position);
            yield return new WaitForSeconds(0.5f);
            if (Vector3.Distance(transform.position, playerRef.transform.position) <= 1.5f)
            {
                playerRef.GetComponent<PlayerHealth>().TakeDamage(1);
                //TODO add animation
            }
        }
        OnPlayerLost();
        yield break;
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
    
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck(transform);
            if (!canSeePlayer) FieldOfViewCheck(headCheckingFOVPoint);
        }
    }

    private IEnumerator CheckPlayersLastPosition(Vector3 _lastKnownPosition)
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(_lastKnownPosition);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, navMeshAgent.destination) < 0.5f);
        yield return new WaitForSeconds(2f);
        currentChickenState = ChickenStates.Patroling;
        StartCoroutine(Patrol());
    }
    
    private void OnPlayerDetected()
    {
        StartCoroutine("StartDetectingPlayer");
    }

    private void OnPlayerLost()
    {
        StartCoroutine(CheckPlayersLastPosition(playerRef.transform.position));
        detectionTimerProgressBar.fillAmount = 0;
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

                if (!Physics.Raycast(_fovPoint.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    if (canSeePlayer == false)
                    {
                        OnPlayerDetected();
                    }
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