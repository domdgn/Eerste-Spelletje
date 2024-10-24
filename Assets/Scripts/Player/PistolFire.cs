using UnityEngine;
using System.Collections;
using TMPro;

public class PistolFire : MonoBehaviour
{
    public Transform weaponHold;

    public GameObject bulletPrefab;
    private PlayerFire playerFire;
    public Transform firePoint;

    public float bulletSpeed = 20f;

    public float bulletLifetime = 2.5f;

    public float fireRate = 0.15f;
    public float defaultFireRate = 0.15f;

    public bool fireOnPress = true;
    public AudioClip shootSound;
    public AudioSource audioSource;
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);

    private bool isFiring = false;
    private float nextFireTime = 0f;
    private Coroutine firingCoroutine;

    private PlayerMovement playerMovement;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerFire = player.GetComponent<PlayerFire>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerFire.currentWeapon = "Pistol";

            weaponHold = player.transform.Find("WeaponHold");
            if (weaponHold != null)
            {
                if (weaponHold.childCount > 0)
                {
                    Destroy(weaponHold.GetChild(0).gameObject);
                }

                transform.SetParent(weaponHold);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                transform.position = weaponHold.position;
                transform.rotation = weaponHold.rotation;
            }
        }
    }

    void Start()
    {
        StopAllCoroutines();
        isFiring = false;
        nextFireTime = 0f;
        firingCoroutine = null;
    }

    private void Update()
    {
        if (!playerMovement.isMovementBlocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartFiring();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopFiring();
            }
        }
    }

    private void StartFiring()
    {
        if (isFiring) return; // Prevent multiple coroutines
        isFiring = true;

        // Only fire immediately if allowed and enough time has passed
        if (fireOnPress && Time.time >= nextFireTime)
        {
            FireBullet();
        }

        firingCoroutine = StartCoroutine(FireRoutine());
    }

    private void StopFiring()
    {
        isFiring = false;

        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireRoutine()
    {
        while (isFiring)
        {
            fireRate = defaultFireRate * (1/playerFire.fireRateMultiplier);
            // Wait until next fire time
            while (Time.time < nextFireTime)
            {
                yield return null;
            }

            // Don't fire if just fired on press
            if (!fireOnPress || Time.time >= nextFireTime + fireRate)
            {
                FireBullet();
            }

            // Use WaitForSeconds for the remaining time if any
            float remainingTime = nextFireTime + fireRate - Time.time;
            if (remainingTime > 0)
            {
                yield return new WaitForSeconds(remainingTime);
            }
        }
    }

    private void FireBullet()
    {
        if (!bulletPrefab || !firePoint) return;

        // Update next fire time
        nextFireTime = Time.time + fireRate;

        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet prefab missing Rigidbody component!");
        }

        // Setup destruction
        Destroy(bullet, bulletLifetime);

        // Play sound
        PlayShootSound();
    }

    private void PlayShootSound()
    {
        if (audioSource && shootSound)
        {
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnDisable()
    {
        StopFiring();
    }
}