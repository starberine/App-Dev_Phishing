using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Behind and above the player

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate desired camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Always look at the player
        transform.LookAt(target);
    }
}
