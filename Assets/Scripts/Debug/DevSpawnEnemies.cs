using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevSpawnEnemies : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject healthkitPrefab;
    public GameObject spawns;
    public GameObject debugText;
    private UnlockedWeapons unlockedWeapons;
    private Camera mainCamera;


    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        unlockedWeapons = gameManager.GetComponent<UnlockedWeapons>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnHealthKitAtCursor();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            unlockedWeapons.UnlockAllWeapons();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(spawns.activeInHierarchy)
            {
                spawns.SetActive(false);
            }
            else
            {
                spawns.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (debugText.activeInHierarchy)
            {
                debugText.SetActive(false);
            }
            else
            {
                debugText.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    /* void SpawnBombAtCursor()
    {
        // Get the cursor position in screen space
        Vector3 mousePos = Input.mousePosition;
        // Convert the cursor position to world space
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Instantiate the bomb at the hit point
            Instantiate(bombPrefab, hit.point, Quaternion.identity);
            print("Bomb spawned");
        }
    } */

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
