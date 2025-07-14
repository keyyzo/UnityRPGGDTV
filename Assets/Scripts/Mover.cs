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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //agent.speed *= moveSpeed;
    }

    private void Update()
    {
        //MoveTowardsTarget();
        ClickToMove();
    }

    private void ClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
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

    void MoveTowardsTarget()
    {
        if (targetTransform != null)
        { 
            agent.destination = targetTransform.position;
            
        }
    }
}
