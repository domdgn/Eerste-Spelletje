using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevSpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject healthkitPrefab;
    private Camera mainCamera;
    public GameObject startText;
    

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.UnloadSceneAsync("ShopMenu");
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
            print("Enemy spawned");
        }
    }

    void SpawnHealthKitAtCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        Vector3 offset = new Vector3(0,1,0);

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(healthkitPrefab, hit.point + offset, Quaternion.identity);
            print("Health Kit spawned");
        }
    }
}
