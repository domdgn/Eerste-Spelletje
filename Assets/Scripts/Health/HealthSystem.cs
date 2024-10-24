using System;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public bool isPlayerDead = false;
    private AudioSource audioSource;
    private HealthBar healthBar;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        // Get the HealthBar component from the child Canvas in the enemy prefab
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
            GameObject audioObject = new GameObject("HurtSound");
            audioObject.tag = "HurtSound";
            PlayHurtSound();
            Destroy(audioObject, hurtSound.length);
        }
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void PlayHurtSound()
    {
        GameObject[] audioObjects = GameObject.FindGameObjectsWithTag("HurtSound");

        if (audioObjects.Length <= 1)
        {
            GameObject audioObject = new GameObject("HurtSound");
            AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
            audioSourceTemp.clip = hurtSound;
            audioSourceTemp.Play();
            Destroy(audioObject, hurtSound.length);
        }
    }

    void Die()
    {
        GameManager.instance.IncrementKillCount();
        GameObject audioObject = new GameObject("DeathSound");
        AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
        audioSourceTemp.clip = deathSound;
        audioSourceTemp.volume = audioSourceTemp.volume * 0.15f;

        audioSourceTemp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);

        audioSourceTemp.Play();
        Destroy(audioObject, deathSound.length);

        Destroy(gameObject);
    }
}
