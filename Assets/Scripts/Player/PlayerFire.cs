using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;     
    public float bulletSpeed = 20f; 
    private float bulletDestroyTime = 2.5f; // Time after shot to destroy bullet (seconds)
    public AudioClip shootSound;
    public AudioSource audioSource;

    void Update()
    {
        // Check if LMB is pressed
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
            audioSource.PlayOneShot(shootSound);
        }
    }

    void FireBullet()
    {
        // Instantiate the bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Add force to the bullet's Rigidbody component to make it move forward
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, bulletDestroyTime);
    }
}
