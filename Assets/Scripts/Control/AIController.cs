using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistanceRadius = 5f;
        [SerializeField] float suspiciousTime = 3f;


        // cached components

        Mover mover;
        Fighter fighter;
        Health health;
        ActionScheduler actionScheduler;

        // Cached player object

        GameObject playerObj;

        // Private variables

        Vector3 guardPosition;
        Vector3 playerLastSeenPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
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

                timeSinceLastSawPlayer = 0.0f;
                AttackBehaviour();

            }

            else if(timeSinceLastSawPlayer < suspiciousTime)
            {
                // Suspicion state
                SuspicionBehaviour();
            }

            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            fighter.Cancel();
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
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
