using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    [SerializeField] private float bombTime = 3f;
    [SerializeField] private float bombDamageTime = 1f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioClip explosionSound;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(BombTimer(bombTime));
    }

    private IEnumerator BombTimer(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(DamageTimer(bombDamageTime));
    }

    private IEnumerator DamageTimer(float damageTime)
    {
        meshRenderer.enabled = false;
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Check for all enemies in the explosion radius
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.Die();
                }
            }
        }

        yield return new WaitForSeconds(damageTime + explosionSound.length);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}