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
            AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
            audioSourceTemp.clip = hurtSound;
            audioSourceTemp.Play();
            Destroy(audioObject, hurtSound.length);
        }
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.IncrementKillCount();
            GameObject audioObject = new GameObject("DeathSound");
            AudioSource audioSourceTemp = audioObject.AddComponent<AudioSource>();
            audioSourceTemp.clip = deathSound;
            audioSourceTemp.Play();
            Destroy(audioObject, deathSound.length);
        }

        if (gameObject.CompareTag("Player"))
        {
            isPlayerDead = true;
        }

        Destroy(gameObject);
    }
}
