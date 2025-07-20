using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100.0f;

        public void TakeDamage(float damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0f);

            Debug.Log(health);

            
        }
    }
}
