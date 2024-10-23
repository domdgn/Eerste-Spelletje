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
            PlayerHealth healthSystem = other.GetComponent<PlayerHealth>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }

    }
}
