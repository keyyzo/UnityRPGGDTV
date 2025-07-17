using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform targetTransform;

        private void LateUpdate()
        {
            UpdateCameraPosition();
        }

        void UpdateCameraPosition()
        {
            if (targetTransform != null)
            {
                transform.position = targetTransform.position;
            }
        }
    }
}
