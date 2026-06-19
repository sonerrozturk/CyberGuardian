using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Camera Limits")]
    public float minX = 0f;
    public float maxX = 12f;
    public float minY = 0.6f;
    public float maxY = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Where the camera normally wants to go
            Vector3 desiredPosition = target.position + offset;

            // Smooth sliding effect
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Clamping X and Y axes within the given limits
            float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Apply the new clamped position (Z axis remains untouched so the view doesn't disappear)
            transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }
    }
}