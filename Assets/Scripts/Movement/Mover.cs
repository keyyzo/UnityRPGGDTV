
using RPG.Core;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform targetTransform;
        [SerializeField] float maxSpeed = 6.0f;

        // Private variables

        Ray lastRay;

        // Cache components

        NavMeshAgent agent;
        Animator animator;
        ActionScheduler actionScheduler;
        Health health;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            agent.enabled = !health.IsDead;

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.destination = destination;
            agent.isStopped = false;
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        

        void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // converts world space velocity to local space velocity

            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        
    }
}
