using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1.25f;
        [SerializeField] float weaponDamage = 5f;


        // Private variables

        Transform target;
        float timeSinceLastAttack = 0.0f;

        // Cached components

        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (!IsInRange())
            {
                mover.MoveTo(target.position);

            }

            else
            {
                mover.Cancel();
                AttackBehaviour();

            }
        }

        

        public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(Target targetToAttack)
        {
            actionScheduler.StartAction(this);
            target = targetToAttack?.transform;

            
        }

        public void Cancel()
        { 
            target = null;
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {

                // This will trigger the Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0.0f;
                
                
            }

        }

        // Animation Event
        void Hit()
        {
            if (target.TryGetComponent(out Health targetHealth))
            {
                targetHealth?.TakeDamage(weaponDamage);
            }
        }

    }
}
