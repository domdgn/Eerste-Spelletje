using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private PlayerHealth healthSystem;
    private Slider healthSlider;

    void Start()
    {
        gameObject.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            healthSystem = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }

        healthSlider = GetComponent<Slider>();
    }

    void Update()
    {
        if (healthSystem != null && healthSlider != null)
        {
            healthSlider.value = healthSystem.currentHealth;
        }

        if (healthSystem.isPlayerDead)
        {
            gameObject.SetActive(false);
        }
    }
}
