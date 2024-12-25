using System;
using System.Collections;
using Stealth;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using Random = UnityEngine.Random;

[Serializable]
public enum ChickenStates
{
    Patroling,
    ObservingPlayer,
    CheckingLastPosition,
    Chase,
    Distracted,
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

    [SerializeField] private float normalSpeed;
    [SerializeField] private float chaseSpeed;
    
    
    
    
    [Space] [SerializeField] private FOVCheck fovCheck;
    [Space] [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private bool canAttack = true;
    
    private int currentPatrolPointIndex = 0;
    [SerializeField] private NavMeshAgent nmAgent;
    [SerializeField] private int damage = 1;

    [Space] [Tooltip("Where to go if got distracted")] [SerializeField] private Transform distractionPoint;
    [SerializeField] private float TimeOfDistraction = 5f;
    [SerializeField] private float TimeOfDistractionDelta = 1f;
    
    [Space] [SerializeField] private Animator animator;


    public ChickenStates GetCurrentState
    {
        get
        {
            return currentChickenState;
        }
    }

    private void Start()
    {
        GetComponent<NavMeshAgent>().speed = normalSpeed;
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
                //check coroutine "ObservingPlayerCoroutine"
                break;
            case ChickenStates.Distracted:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator ObservingPlayerCoroutine()
    {
        detectionMonitor.SetToDetected();
        animator.SetBool("isStaying", true);
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
                GetComponent<NavMeshAgent>().speed = normalSpeed;
                detectionMonitor.PlayerLost();
                yield break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        nmAgent.isStopped = false;
        currentChickenState = ChickenStates.Chase;
        GetComponent<NavMeshAgent>().speed = chaseSpeed;
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
                animator.SetBool("isStaying", false);
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
            //currentChickenState = ChickenStates.ObservingPlayer;
            //StartCoroutine(ObservingPlayerCoroutine());
            currentChickenState = ChickenStates.Chase;
            detectionMonitor.SetDetectingValue(1);
        }
        else
        {
            if (nmAgent.remainingDistance <= 0.5f)
            {
                currentChickenState = ChickenStates.Patroling;
                GetComponent<NavMeshAgent>().speed = normalSpeed;
            }
        }
    }

    public void Distract()
    {
        StopAllCoroutines();
        StartCoroutine(GetDistractedCoroutine());
    }
    
    private IEnumerator GetDistractedCoroutine()
    {
        currentChickenState = ChickenStates.Distracted;
        nmAgent.SetDestination(distractionPoint.position);
        nmAgent.isStopped = false;
        yield return new WaitUntil( () =>
        {
            return nmAgent.isStopped;
        });
        yield return new WaitForSeconds(TimeOfDistraction + Random.Range(-TimeOfDistractionDelta, TimeOfDistractionDelta));
        currentChickenState = ChickenStates.Patroling;
        GetComponent<NavMeshAgent>().speed = normalSpeed;
    }
    
    private void Attack()
    {
        if (canAttack)
        {
            player.GetComponentInParent<PlayerHealth>().TakeDamage(damage, DamageImpactSoundType.Scream);
            animator.SetTrigger("Attack");
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