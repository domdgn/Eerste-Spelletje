using UnityEngine;

public class RestartSplashAppear : MonoBehaviour
{
    public PlayerHealth playerHealthSystem;
    public GameObject restartSplash;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerHealthSystem = playerObject.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (playerHealthSystem.currentHealth <= 0)
        {
            Debug.Log("Nee! De speler is gevallen! Hij ziet er zo slecht uit man."); 
            restartSplash.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
