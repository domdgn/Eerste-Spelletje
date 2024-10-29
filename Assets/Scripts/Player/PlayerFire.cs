using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private UnlockedWeapons unlockedWeapons;

    public float fireRateMultiplier = 1f;
    public float defaultFireRateMultiplier = 1f;

    public string currentWeapon;

    private PistolFire pistol;

    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;
    public GameObject bombPrefab;

    private Camera mainCamera;

    private bool canPlaceBomb = true;
    public float bombCooldownDuration = 5f;

    void Awake()
    {
        mainCamera = Camera.main;
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        unlockedWeapons = gameManager.GetComponent<UnlockedWeapons>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnPistol();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && unlockedWeapons.shotgunUnlocked)
        {
            SpawnShotgun();
        }

        if (Input.GetMouseButtonDown(2) && unlockedWeapons.bombUnlocked)
        {
            PlaceBomb();
        }

    }

    void SpawnPistol()
    {
        int pistolCount = GameObject.FindGameObjectsWithTag("Pistol").Length;
        if (pistolCount < 1)
        {
            Instantiate(pistolPrefab, Vector3.zero, Quaternion.identity);
            print("Pistol equipped");
        }
        else
        {
            print("Pistol already equipped");
            return;
        }
    }

    void SpawnShotgun()
    {
        int shotgunCount = GameObject.FindGameObjectsWithTag("Shotgun").Length;
        if (shotgunCount < 1)
        {
            Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
            print("Shotgun equipped");
        }
        else
        {
            print("Shotgun already equipped");
            return;
        }
    }

    void PlaceBomb()
    {
        if (!canPlaceBomb || GameObject.FindGameObjectsWithTag("Bomb").Length >= 1)
        {
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 spawnPosition = hit.point + new Vector3(0, 0.5f, 0);
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            print("Bomb spawned");

            StartCoroutine(BombCooldown());
        }
    }

    IEnumerator BombCooldown()
    {
        canPlaceBomb = false;
        yield return new WaitForSecondsRealtime(bombCooldownDuration);
        canPlaceBomb = true;
    }
}
