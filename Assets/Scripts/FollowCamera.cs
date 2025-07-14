using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    private void Update()
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
