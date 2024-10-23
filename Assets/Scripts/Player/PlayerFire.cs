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

    private bool isFiring = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
            isFiring = true;
            StartCoroutine(BulletTimer());
        } 

        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
            StopCoroutine(BulletTimer());
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(shootSound);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, bulletDestroyTime);
    }

    IEnumerator BulletTimer()
    {
        while (isFiring)
        {
            yield return new WaitForSeconds(0.25f);
            FireBullet();
        }
    }
}
