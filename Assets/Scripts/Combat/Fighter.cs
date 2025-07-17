using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;


        // Private variables

        Transform target;

        // Cached components

        Mover mover;
        ActionScheduler actionScheduler;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (target == null)
                return;

            if (!IsInRange())
            {
                mover.MoveTo(target.position);

            }

            else
            {
                mover.Cancel();
            }
        }

        public bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(Target targetToAttack)
        {
            actionScheduler.StartAction(this);
            target = targetToAttack.transform;
        }

        public void Cancel()
        { 
            target = null;
        }

    }
}
