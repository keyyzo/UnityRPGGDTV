
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform targetTransform;
        [SerializeField] float moveSpeed = 2.0f;

        // Private variables

        Ray lastRay;

        // Cache components

        NavMeshAgent agent;
        Animator animator;
        ActionScheduler actionScheduler;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Start()
        {

        }

        private void Update()
        {

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
            
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
