using UnityEngine;

public class HealthBarOnPlayer : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

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

    void Update()
    {
        if (player != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
            transform.position = screenPos;
        }
    }
}
