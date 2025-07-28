using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistanceRadius = 5f;
        [SerializeField] float suspiciousTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellingTime = 3f;

        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;


        // cached components

        Mover mover;
        Fighter fighter;
        Health health;
        ActionScheduler actionScheduler;
        NavMeshAgent agent;

        // Cached player object

        GameObject playerObj;

        // Private variables

        Vector3 guardPosition;

        int currentWaypointIndex = 0;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArriveAtWaypoint = Mathf.Infinity;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            playerObj = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead)
                return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(playerObj))
            {
                Debug.Log(gameObject.name + " can give chase");

                
                AttackBehaviour();

            }

            else if (timeSinceLastSawPlayer < suspiciousTime)
            {
                // Suspicion state
                SuspicionBehaviour();
            }

            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            fighter.Cancel();

            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    
                    CycleWaypoint();
                }


                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArriveAtWaypoint > waypointDwellingTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            timeSinceArriveAtWaypoint = 0.0f;
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0.0f;
            fighter.Attack(playerObj);
        }

        private bool InAttackRangeOfPlayer()
        {
            //GameObject playerObj = GameObject.FindWithTag("Player");

            float distanceToPlayer = Vector3.Distance(transform.position, playerObj.transform.position);
            return distanceToPlayer < chaseDistanceRadius;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistanceRadius);
        }
    }
}
