using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {


        // Cache components

        Mover mover;
        Fighter fighter;
        Health health;
        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead)
                return;

            if (InteractWithCombat())
                return;

            if (InteractWithMovement())
                return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.TryGetComponent(out Target targetToHit))
                {
                    
                    if (!fighter.CanAttack(targetToHit.gameObject)) continue;

                    if (Input.GetMouseButtonDown(0))
                    {
                        fighter.Attack(targetToHit.gameObject);

                    }
                    return true;
                }
            }

            return false;
        } 

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mover.StartMoveAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private void MoveToCursor()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.MoveTo(hit.point);
                }

                
            }

        } // NO LONGER USED, KEEPING FOR POTENTIAL BACKTRACKING

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
