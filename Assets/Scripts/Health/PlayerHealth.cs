using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip healSound;
    public bool isPlayerDead = false;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        PlayHurtSound();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void HealthPickup(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        GameObject audioObject = new GameObject("HealSound");
        AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
        audioSourceTemp.clip = healSound;
        audioSourceTemp.Play();
        Destroy(audioObject, healSound.length);
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

    public void Die()
    {
        isPlayerDead = true;
        Destroy(gameObject);
    }
}
