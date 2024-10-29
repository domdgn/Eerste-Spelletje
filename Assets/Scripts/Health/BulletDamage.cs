using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public GameObject particlePrefab;
    public float damageAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damageAmount);
            }

            // Destroy the bullet & create particle effect after it hits something
            GameObject poof = Instantiate(particlePrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            GameObject poof = Instantiate(particlePrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }

    }
}
