using UnityEngine;

public class PlayerController : MonoBehaviour
{


    // Cache components

    Mover mover;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ClickToMove();
        }
    }

    private void ClickToMove()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            mover.MoveTo(hit.point);
        }

    }
}
