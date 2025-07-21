using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistanceRadius = 5f;


        // cached components

        Mover mover;
        Fighter fighter;
        Health health;

        // Cached player object

        GameObject playerObj;

        // Private variables

        Vector3 guardPosition;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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

                fighter.Attack(playerObj);

            }

            else
            {
                fighter.Cancel();
                mover.StartMoveAction(guardPosition);
            }
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
