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

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

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

            if (target.IsDead)
                return;
            

            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position);

            }

            else
            {
                mover.Cancel();
                AttackBehaviour();

            }
        }

        

        public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject targetToAttack)
        {
            if(targetToAttack == null)
                return false;

            Health targetHealth = targetToAttack.GetComponent<Health>();

            return targetHealth != null && !targetHealth.IsDead;
        }

        public void Attack(GameObject targetToAttack)
        {
            actionScheduler.StartAction(this);
            if (targetToAttack != null && targetToAttack.TryGetComponent(out Health targetHealth))
            {
                target = targetHealth;
            }
            

            
        }

        public void Cancel()
        {
            ResetAttack();
            target = null;
        }

        private void ResetAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0.0f;


            }

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }



        // Animation Event
        void Hit()
        {
            //if (target.TryGetComponent(out Health targetHealth))
            //{
            //    targetHealth?.TakeDamage(weaponDamage);
            //}

            target?.TakeDamage(weaponDamage);
        }

    }
}
