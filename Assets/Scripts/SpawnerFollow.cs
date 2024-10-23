using UnityEngine;

public class SpawnerFollow : MonoBehaviour
{
    public Vector3 offset;
    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the target position
            Vector3 targetPosition = player.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
