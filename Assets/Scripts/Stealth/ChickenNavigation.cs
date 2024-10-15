using System;
using System.Collections;
using Stealth;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public enum ChickenStates
{
    Patroling,
    ObservingPlayer,
    CheckingLastPosition,
    Chase,
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FOVCheck))]
public class ChickenNavigation : MonoBehaviour
{
    [SerializeField] private ChickenStates currentChickenState;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerSafeZoneChecker safezoneChecker;
    [SerializeField] [Tooltip("In centiseconds (1 сs = 0,01s)")] private int timeToDetectPlayer;
    [SerializeField] private DetectionUIMonitor detectionMonitor;
    
    
    [Space] [SerializeField] private FOVCheck fovCheck;
    [Space] [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private bool canAttack = true;
    
    private int currentPatrolPointIndex = 0;
    [SerializeField] private NavMeshAgent nmAgent;
    [SerializeField] private int damage = 1;


    private void OnValidate()
    {
        if (!fovCheck)
        {
            fovCheck = GetComponent<FOVCheck>();
        }

        if (!safezoneChecker)
        {
            safezoneChecker = FindObjectOfType<PlayerSafeZoneChecker>();
        }
    }

    private void FixedUpdate()
    {
        switch (currentChickenState)
        {
            case ChickenStates.Patroling:
                PatrolingIteration();
                break;
            case ChickenStates.CheckingLastPosition:
                CheckLastPositionIteration();
                break;
            case ChickenStates.Chase:
                ChaseIteration();
                break;
            case ChickenStates.ObservingPlayer:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator ObservingPlayerCoroutine()
    {
        detectionMonitor.SetToDetected();
        nmAgent.isStopped = true;
        for (int i = 0; i < timeToDetectPlayer; i++)
        {
            transform.LookAt(player, Vector3.up);
            if (fovCheck.Detect(player))
            {
                detectionMonitor.SetDetectingValue((float)i/(float)timeToDetectPlayer);
            }
            else
            {
                nmAgent.isStopped = false;
                currentChickenState = ChickenStates.Patroling;
                detectionMonitor.PlayerLost();
                yield break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        nmAgent.isStopped = false;
        currentChickenState = ChickenStates.Chase;
    }
    
    /// <summary>
    /// Being invoked every physical frame if the chicken is in Patroling state
    /// </summary>
    private void PatrolingIteration()
    {
        if (fovCheck.Detect(player) && !safezoneChecker.PlayerIsInSafeZone)
        {
            currentChickenState = ChickenStates.ObservingPlayer;
            StartCoroutine(ObservingPlayerCoroutine());
        }
        else
        {
            if (nmAgent.remainingDistance <= 0.5f)
            {
                currentPatrolPointIndex =
                    patrolPoints.Length-1 > currentPatrolPointIndex ? currentPatrolPointIndex + 1 : 0;
                nmAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
            }
        }
    }

    /// <summary>
    /// Being invoked every physical frame if the chicken is in Chase state 
    /// </summary>
    private void ChaseIteration()
    {
        if ((fovCheck.Detect(player) && !safezoneChecker.PlayerIsInSafeZone) || (Vector3.Distance(player.position, transform.position) <= 5 && !safezoneChecker.PlayerIsInSafeZone))
        {
            if (Vector3.Distance(new Vector3(player.position.x, transform.position.y, player.position.z), transform.position) <= 1f)
            {
                Attack();
            }
            else
            {
                nmAgent.isStopped = false;
                nmAgent.SetDestination(player.position);
            }
        }
        else
        {
            detectionMonitor.PlayerLost();
            nmAgent.isStopped = false;
            currentChickenState = ChickenStates.CheckingLastPosition;
        }
    }

    /// <summary>
    /// Being invoked every physical frame if the chicken is in CheckingLastPosition state 
    /// </summary>
    private void CheckLastPositionIteration()
    {
        if (fovCheck.Detect(player) && !safezoneChecker.PlayerIsInSafeZone)
        {
            currentChickenState = ChickenStates.ObservingPlayer;
            StartCoroutine(ObservingPlayerCoroutine());
        }
        else
        {
            if (nmAgent.remainingDistance <= 0.5f)
            {
                currentChickenState = ChickenStates.Patroling;
            }
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            player.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
        yield break;
    }
    
    
}