using UnityEngine;

public class CircleWalker : MonoBehaviour
{
    public float radius = 10f; // Set the radius of the circle
    public float speed = 2f;  // Speed of movement
    public float angle = 0f;
    private Vector3 centerPosition;

    void Start()
    {
        centerPosition = transform.position; // Save the initial position as the center
    }

    void Update()
    {
        // Calculate the new position around the center
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        Vector3 newPosition = new Vector3(x, 0, z) + centerPosition;
        transform.position = newPosition;

        // Increase the angle over time
        angle += speed * Time.deltaTime;

        // Rotate the object to face the direction of travel, offset by -90 degrees on the Y axis
        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);
        transform.rotation = rotation;
    }
}
