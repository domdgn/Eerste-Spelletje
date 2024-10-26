using UnityEngine;
using System.Collections;
using TMPro;

public class ShotgunFire : MonoBehaviour
{
    public Transform weaponHold;
    public GameObject singleBulletPrefab;
    public GameObject doubleBulletPrefab;
    private PlayerFire playerFire;
    public Transform firePoint;

    [Header("Weapon Properties")]
    public float bulletSpeed = 15f;
    public float bulletLifetime = 5f;
    private float singleShotCooldown = 0.5f;
    public float defaultSingleShotCooldown = 0.5f;
    private float doubleShotCooldown = 0.75f;
    public float defaultDoubleShotCooldown = 0.75f;

    [Header("Single Shot Properties")]
    public int singleShotPellets = 4;  // Fewer pellets for single shot
    public float singleShotSpread = 7f;  // Tighter spread angle

    [Header("Double Shot Properties")]
    public int doubleShotPellets = 8;  // More pellets for double shot
    public float doubleShotSpread = 15f;  // Wider spread angle

    [Header("Audio")]
    public AudioClip shootSoundSingle;
    public AudioClip shootSoundDouble;
    public AudioSource audioSource;
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);

    private bool canFire = true;
    private PlayerMovement playerMovement;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            SetupWeapon(player);
        }
    }

    private void SetupWeapon(GameObject player)
    {
        playerFire = player.GetComponent<PlayerFire>();
        playerFire.currentWeapon = "Shotgun";
        weaponHold = player.transform.Find("WeaponHold");

        if (weaponHold != null)
        {
            ClearExistingWeapon();
            AttachWeapon();
        }
    }

    private void ClearExistingWeapon()
    {
        if (weaponHold.childCount > 0)
        {
            Destroy(weaponHold.GetChild(0).gameObject);
        }
    }

    private void AttachWeapon()
    {
        transform.SetParent(weaponHold);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.position = weaponHold.position;
        transform.rotation = weaponHold.rotation;
    }

    private void Update()
    {
        // BLOCK MOVEMENT PLS
        if (!canFire) return;
        if (playerMovement.isMovementBlocked) return;

        if (Input.GetMouseButton(0))
        {
            FireSingle();
        }
        else if (Input.GetMouseButton(1))
        {
            FireDouble();
        }

        singleShotCooldown = defaultSingleShotCooldown * (1 / playerFire.fireRateMultiplier);
        doubleShotCooldown = defaultDoubleShotCooldown * (1 / playerFire.fireRateMultiplier);
    }

    private void FireSingle()
    {
        if (!singleBulletPrefab || !firePoint) return;

        // Fire multiple pellets with tighter spread
        FireSpreadPattern(singleBulletPrefab, singleShotPellets, singleShotSpread);
        PlayShootSound(shootSoundSingle);
        StartCoroutine(FireCooldown(singleShotCooldown));
    }

    private void FireDouble()
    {
        if (!doubleBulletPrefab || !firePoint) return;

        // Fire more pellets with wider spread
        FireSpreadPattern(doubleBulletPrefab, doubleShotPellets, doubleShotSpread);
        PlayShootSound(shootSoundDouble);
        StartCoroutine(FireCooldown(doubleShotCooldown));
    }

    private void FireSpreadPattern(GameObject bulletPrefab, int pelletCount, float spreadAngle)
    {
        // Calculate center point for spread pattern
        Vector3 centerPoint = firePoint.forward;

        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate spread using a circular pattern
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(0f, spreadAngle);

            //float x = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;

            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, y, 0);

            FireBullet(bulletPrefab, firePoint.position, spreadRotation);
        }
    }

    private void FireBullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = rotation * Vector3.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet prefab missing Rigidbody component!");
        }
        Destroy(bullet, bulletLifetime);
    }

    private void PlayShootSound(AudioClip clip)
    {
        if (audioSource && clip)
        {
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator FireCooldown(float cooldownTime)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
    }
}