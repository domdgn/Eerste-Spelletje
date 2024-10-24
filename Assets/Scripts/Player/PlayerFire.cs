using System.Security.Cryptography;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public bool hasShotgunUnlocked = false;

    public float fireRateMultiplier = 1f;
    public float defaultFireRateMultiplier = 1f;

    public string currentWeapon;

    private PistolFire pistol;

    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnPistol();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasShotgunUnlocked)
        {
            SpawnShotgun();
        }
    }

    void SpawnPistol()
    {
        Instantiate(pistolPrefab, Vector3.zero, Quaternion.identity);
        print("Pistol spawned");
    }

    void SpawnShotgun()
    {
        Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
        print("Shotgun spawned");
    }
}
