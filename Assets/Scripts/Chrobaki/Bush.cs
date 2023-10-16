using System.Collections;
using System.Collections.Generic;
using Chrobaki;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Bush : MonoBehaviour
{
    [SerializeField] public int bushMaxHealth = 30;
    [HideInInspector] public bool hasBugOnIt;
    [SerializeField] public int bushHealth;
    [SerializeField] private List<Transform> bugPositions = new List<Transform>();
    [HideInInspector] public List<Bug> bugsOnTheBush = new List<Bug>();
    [SerializeField] private List<GameObject> startBugs = new List<GameObject>();
    [SerializeField] private float probabilityOfSpawningBug = 0.2f;
    [SerializeField] public Transform playerMarker;
    [SerializeField] [HideInInspector] private Transform player;

    private void OnValidate()
    {
        player = FindObjectOfType<PlayerInput>().transform;
    }

    private void Start()
    {
        if (Random.Range(0f, 1f) < probabilityOfSpawningBug)
        {
            var index = Random.Range(0, startBugs.Count);
            startBugs[index].SetActive(true);
            bugsOnTheBush.Add(startBugs[index].GetComponent<Bug>());
            FindObjectOfType<BugSpawner>().AddBugManually();
            hasBugOnIt = true;
            playerMarker.gameObject.SetActive(true);
            StartCoroutine(ReduceHealth());
        }
    }

    private void Update()
    {
        if (hasBugOnIt)
        {
            playerMarker.SetPositionAndRotation(player.transform.position, Quaternion.identity);
            playerMarker.LookAt(new Vector3(transform.position.x, playerMarker.position.y, transform.position.z));
        }
    }

    private void Awake()
    {
        bushHealth = bushMaxHealth;
    }

    public Transform GetBugPosition()
    {
        return bugPositions[Random.Range(0, bugPositions.Count)];
    }

    public void StopHitting()
    {
        StopAllCoroutines();
        hasBugOnIt = false;
        for (var index = 0; index < bugsOnTheBush.Count; index++)
        {
            var VARIABLE = bugsOnTheBush[index];
            VARIABLE.StopAllCoroutines();
            VARIABLE.BugDeath();
        }
        playerMarker.gameObject.SetActive(false);
        bugsOnTheBush = new List<Bug>();
    }

    private IEnumerator ReduceHealth()
    {
        do
        {
            yield return new WaitForSeconds(1f);
            bushHealth--;
            if (bushHealth <= 0)
            {
                for (var index = 0; index < bugsOnTheBush.Count; index++)
                {
                    Bug bug = bugsOnTheBush[index];
                    bug.BugDeath();
                }

                var bushArray = FindObjectOfType<BushArray>();
                bushArray.bushes.Remove(this);
                Destroy(gameObject);
                yield break;
            }
        } while (hasBugOnIt);
    }
}