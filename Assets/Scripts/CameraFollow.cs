using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset; // Set the desired offset from the player
    private Transform player;

    void Start()
    {
        // Find the player object by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }

        // Ensure the camera is orthographic
        Camera.main.orthographic = true;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position
            Vector3 targetPosition = player.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

            // Keep the camera's rotation stable
            transform.rotation = Quaternion.Euler(90, 0, 0); // Ensures top-down view
        }
    }
}
