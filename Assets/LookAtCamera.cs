using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 direction = cam.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Try 180 first. If wrong, try 90 or -90.
            transform.rotation = lookRotation * Quaternion.Euler(0, 90f, 0);
        }
    }
}