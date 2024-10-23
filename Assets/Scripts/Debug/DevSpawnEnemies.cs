using UnityEngine;

public class DevSpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject healthkitPrefab;
    private Camera mainCamera;
    public GameObject startText;

    void Start()
    {
        mainCamera = Camera.main;
        startText.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemyAtCursor();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnHealthKitAtCursor();
        }
    }

    void SpawnEnemyAtCursor()
    {
        // Get the cursor position in screen space
        Vector3 mousePos = Input.mousePosition;
        // Convert the cursor position to world space
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Instantiate the enemy at the hit point
            Instantiate(enemyPrefab, hit.point, Quaternion.identity);
        }
    }

    void SpawnHealthKitAtCursor()
    {
        // Get the cursor position in screen space
        Vector3 mousePos = Input.mousePosition;
        // Convert the cursor position to world space
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        Vector3 offset = new Vector3(0,1,0);

        if (Physics.Raycast(ray, out hit))
        {
            // Instantiate the enemy at the hit point
            Instantiate(healthkitPrefab, hit.point + offset, Quaternion.identity);
        }
    }
}
