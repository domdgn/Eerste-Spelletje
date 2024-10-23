using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }

    }
}
