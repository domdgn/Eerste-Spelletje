using UnityEngine;
using System.Collections.Generic;

public class InfiniteGround : MonoBehaviour
{
    [Header("Ground Settings")]
    [Tooltip("The ground tile prefab to be instantiated")]
    public GameObject groundTilePrefab;

    [Tooltip("Size of each ground tile")]
    public float tileSize = 50f;

    [Tooltip("Number of tiles in each direction (total tiles will be this number squared)")]
    [Range(3, 9)]
    public int tileCount = 3;

    [Header("Performance Settings")]
    [Tooltip("Distance threshold for tile repositioning")]
    public float repositioningThreshold = 1f;

    [Tooltip("How often to check for tile repositioning (in seconds)")]
    public float updateInterval = 0.1f;

    private Transform player;
    private List<Transform> tiles = new List<Transform>();
    private Vector3 lastPlayerPosition;
    private float updateTimer;
    private float halfTileSize;
    private int totalTiles;

    private void Start()
    {
        InitializeSettings();
        FindAndValidatePlayer();
        CreateInitialTiles();
    }

    private void InitializeSettings()
    {
        halfTileSize = tileSize * 0.5f;
        totalTiles = tileCount * tileCount;
        updateTimer = 0f;

        // Ensure tile count is odd for proper centering
        if (tileCount % 2 == 0) tileCount++;
    }

    private void FindAndValidatePlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("InfiniteGround: Player object not found! Please ensure player has the 'Player' tag.");
            enabled = false;
            return;
        }

        player = playerObject.transform;
        lastPlayerPosition = player.position;
    }

    private void CreateInitialTiles()
    {
        int halfCount = tileCount / 2;
        Vector3 centerPosition = new Vector3(
            Mathf.Round(player.position.x / tileSize) * tileSize,
            0f,
            Mathf.Round(player.position.z / tileSize) * tileSize
        );

        for (int x = -halfCount; x <= halfCount; x++)
        {
            for (int z = -halfCount; z <= halfCount; z++)
            {
                Vector3 position = centerPosition + new Vector3(x * tileSize, 0, z * tileSize);
                CreateTile(position);
            }
        }
    }

    private void CreateTile(Vector3 position)
    {
        GameObject tile = Instantiate(groundTilePrefab, position, Quaternion.identity, transform);
        tiles.Add(tile.transform);
    }

    private void Update()
    {
        if (player == null) return;

        updateTimer += Time.deltaTime;
        if (updateTimer < updateInterval) return;

        updateTimer = 0f;

        // Only update if player has moved enough
        if (Vector3.Distance(lastPlayerPosition, player.position) < repositioningThreshold)
            return;

        RepositionTiles();
        lastPlayerPosition = player.position;
    }

    private void RepositionTiles()
    {
        float checkDistance = tileSize * (tileCount / 2);

        foreach (Transform tile in tiles)
        {
            Vector3 offset = player.position - tile.position;
            Vector3 newPosition = tile.position;
            bool needsUpdate = false;

            // Check X axis
            if (Mathf.Abs(offset.x) >= checkDistance)
            {
                newPosition.x += (offset.x > 0 ? 1 : -1) * (tileSize * tileCount);
                needsUpdate = true;
            }

            // Check Z axis
            if (Mathf.Abs(offset.z) >= checkDistance)
            {
                newPosition.z += (offset.z > 0 ? 1 : -1) * (tileSize * tileCount);
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                tile.position = newPosition;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying && groundTilePrefab != null)
        {
            Gizmos.color = Color.green;
            Vector3 centerPos = transform.position;
            float totalSize = tileSize * tileCount;
            Gizmos.DrawWireCube(centerPos, new Vector3(totalSize, 0.1f, totalSize));
        }
    }
}