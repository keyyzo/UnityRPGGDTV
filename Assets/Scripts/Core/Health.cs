using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100.0f;

        Animator animator;
        ActionScheduler actionScheduler;

        bool isDead = false;

        public bool IsDead => isDead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0f);

            if (healthPoints == 0 && !isDead)
            { 

                ProcessDeath();
            }
            
        }

        public void ProcessDeath()
        {
            if (isDead)
                return;

            isDead = true;

            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                ProcessDeath();
            }
        }
    }
}
