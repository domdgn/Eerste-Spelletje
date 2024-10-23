using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public float tileSize = 50f;
    public int tileCount = 3;

    private Transform player;
    private List<Transform> tiles = new List<Transform>();
    private Vector3 lastPlayerPosition;

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
            return;
        }

        // Initialize tiles in a grid around the player
        for (int x = 0; x < tileCount; x++)
        {
            for (int z = 0; z < tileCount; z++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                Transform newTile = Instantiate(groundTilePrefab, position, Quaternion.identity).transform;
                tiles.Add(newTile);
            }
        }

        lastPlayerPosition = player.position;
    }

    void Update()
    {
        if (player == null || tiles.Count == 0) return;

        Vector3 playerMovement = player.position - lastPlayerPosition;
        Vector3 direction = Vector3.zero;

        if (playerMovement.x >= tileSize)
        {
            direction = Vector3.right;
        }
        else if (playerMovement.x <= -tileSize)
        {
            direction = Vector3.left;
        }
        else if (playerMovement.z >= tileSize)
        {
            direction = Vector3.forward;
        }
        else if (playerMovement.z <= -tileSize)
        {
            direction = Vector3.back;
        }

        if (direction != Vector3.zero)
        {
            Transform tileToMove = tiles[0];
            tiles.RemoveAt(0);

            Vector3 newPosition = player.position + direction * tileSize * tileCount;
            newPosition.x = Mathf.Round(newPosition.x / tileSize) * tileSize;
            newPosition.z = Mathf.Round(newPosition.z / tileSize) * tileSize;

            tileToMove.position = newPosition;
            tiles.Add(tileToMove);

            lastPlayerPosition = player.position;
        }
    }
}
