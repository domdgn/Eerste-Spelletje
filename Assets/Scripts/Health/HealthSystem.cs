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
    private bool isDead = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

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

        if (currentHealth <= 0f && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void PlayHurtSound()
    {
        GameObject[] audioObjects = GameObject.FindGameObjectsWithTag("HurtSound");
        if (audioObjects.Length <= 3)
        {
            GameObject audioObject = new GameObject("HurtSound");
            audioObject.tag = "HurtSound";
            AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
            audioSourceTemp.clip = hurtSound;
            audioSourceTemp.Play();
            Destroy(audioObject, hurtSound.length);
        }
    }

    public void Die()
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
