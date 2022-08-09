using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class EnemyFollow : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private NavMeshAgent agent;
        [HideInInspector] [SerializeField] private Transform playerTrarnsform;
        [SerializeField] private float minDistanceToFollowPlayer = 20f;
        [SerializeField] private float speed = 3;
        [SerializeField] private List<Transform> waypoints;
        public bool followPlayer;
        private Transform objectToFollow;
        private Queue<Transform> waypointsLeft = new Queue<Transform>();

        private void OnValidate()
        {
            playerTrarnsform = FindObjectOfType<PlayerInput>().transform;
            agent = GetComponent<NavMeshAgent>();
        }

        public void NextWayPoint()
        {
            if (waypointsLeft.Count < 1)
            {
                for (int i = 0; i < waypoints.Count; i++)
                {
                    waypointsLeft.Enqueue(waypoints[i]);
                }
            }

            objectToFollow = waypointsLeft.Dequeue();
            agent.SetDestination(objectToFollow.position);
        }


        public void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, playerTrarnsform.position) < minDistanceToFollowPlayer)
            {
                followPlayer = true;
            }
            else
            {
                if (followPlayer) NextWayPoint();
                followPlayer = false;
            }
        }

        private void Start()
        {
            NextWayPoint();
        }

        private void Update()
        {
            if (followPlayer)
            {
                agent.destination = playerTrarnsform.position;
            }

            if (Vector3.Distance(transform.position, agent.destination) < 2)
            {
                NextWayPoint();
            }
        }
    }
}