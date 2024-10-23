using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Rapid Fire Rate")]         //LOOK WHAT I LEARNED
    [Tooltip("Adjusts fire rate when powered up")]
    public float rapidRate = 0.8f;
    public bool isRapid = false;

    private PlayerFire playerFire;
    private PlayerHealth playerHealth;

    private void Start()
    {
        isRapid = false;
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        playerFire = gameObject.GetComponent<PlayerFire>();
        playerFire.fireRate = playerFire.defaultFireRate;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RapidFire") && isRapid == false)
        {
            Debug.Log("Rapid Fire Pickup");

            Destroy(other.gameObject);
            StartCoroutine(RapidFireTimer());
        }

        if (other.CompareTag("HealthKit") && playerHealth.currentHealth < 100)
        {
            Debug.Log("Health Pickup");
            playerHealth.HealthPickup(20f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator RapidFireTimer()
    {
        playerFire.fireRate = rapidRate/20;
        isRapid = true;
        yield return new WaitForSecondsRealtime(5);
        isRapid = false;
        playerFire.fireRate = playerFire.defaultFireRate;
    }
}
