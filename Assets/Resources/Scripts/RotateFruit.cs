using UnityEngine;

public class RotateFruit : MonoBehaviour
{
    public float speed = 40f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}