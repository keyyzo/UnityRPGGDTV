using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100.0f;

        Animator animator;

        bool isDead = false;

        public bool IsDead => isDead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
        }
    }
}
