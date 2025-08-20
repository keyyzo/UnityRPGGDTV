
using RPG.Core;
using UnityEditor.Analytics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using System.Collections.Generic;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
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

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            // Dictionary implementation -----------------------------------------

            //Dictionary<string, object> data = new Dictionary<string, object>();

            //data["position"] = new SerializableVector3(transform.position);
            //data["rotation"] = new SerializableVector3(transform.eulerAngles);
            //return data;

            // Struct implementation --------------------------------------

            MoverSaveData dataStruct = new MoverSaveData();
            dataStruct.position = new SerializableVector3(transform.position);
            dataStruct.rotation = new SerializableVector3(transform.eulerAngles);
            return dataStruct;

        }

        public void RestoreState(object state)
        {
            // Dictionary implementation -----------------------------------------

            //Dictionary<string, object> data = (Dictionary<string, object>)state;

            //agent.enabled = false;
            //transform.position = ((SerializableVector3)data["position"]).ToVector();
            //transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            //agent.enabled = true;

            // Struct implementation --------------------------------------

            MoverSaveData data = (MoverSaveData)state;
            agent.enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            agent.enabled = true;

        }
    }
}
