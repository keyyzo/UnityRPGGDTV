using RPG.Combat;
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

        GameObject playerObj;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            playerObj = GameObject.FindWithTag("Player");
        }

        private void Update()
        {


            if (InAttackRangeOfPlayer() && fighter.CanAttack(playerObj))
            {
                Debug.Log(gameObject.name + " can give chase");

                fighter.Attack(playerObj);

            }

            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            //GameObject playerObj = GameObject.FindWithTag("Player");

            float distanceToPlayer = Vector3.Distance(transform.position, playerObj.transform.position);
            return distanceToPlayer < chaseDistanceRadius;
        }

    }
}
