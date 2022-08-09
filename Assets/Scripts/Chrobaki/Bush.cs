using System.Collections;
using System.Collections.Generic;
using Chrobaki;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] public int bushMaxHealth = 30;
    [HideInInspector] public bool hasBugOnIt;
    [SerializeField] public int bushHealth;
    [SerializeField] private List<Transform> bugPositions = new List<Transform>();
    [HideInInspector] public List<Bug> bugsOnTheBush = new List<Bug>();

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
        // for (var index = 0; index < bugsOnTheBush.Count; index++)
        // {
            // var VARIABLE = bugsOnTheBush[index];
            // VARIABLE.BugDeath();
        // }
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
                    Destroy(bug.gameObject);
                }
                Destroy(gameObject);
                yield break;
            }
        } while (hasBugOnIt);
    }
}