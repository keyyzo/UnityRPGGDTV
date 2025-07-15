using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float moveSpeed = 2.0f;

    // Private variables

    Ray lastRay;

    // Cache components

    NavMeshAgent agent;
    Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
        ClickToMove();
        UpdateAnimator();
    }

    private void ClickToMove()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            { 
                agent.destination = hit.point;
            }

            //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            
        }

        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100f);
    }

    void UpdateAnimator()
    { 
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity); // converts world space velocity to local space velocity

        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }

}
