using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool isMainScenePaused = false;
    private PlayerMovement movement;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        

        if (player != null)
        {
            movement = player.GetComponent<PlayerMovement>();
            //Debug.Log("test test woop woop yayayay");
        }
    }

    void Update()
    {
        // Check if the additive scene is loaded
        if (SceneManager.GetSceneByName("ShopMenu").isLoaded && !isMainScenePaused)
        {
            PauseMainScene();
        }

        // Resume the main scene if the additive scene is unloaded
        if (!SceneManager.GetSceneByName("ShopMenu").isLoaded && isMainScenePaused)
        {
            ResumeMainScene();
        }
    }

    void PauseMainScene()
    {
        movement.BlockPlayerMovement(true);
        Time.timeScale = 0f; // Pause the main scene
        isMainScenePaused = true;
    }

    void ResumeMainScene()
    {
        movement.BlockPlayerMovement(false);
        Time.timeScale = 1f; // Resume the main scene
        isMainScenePaused = false;
    }
}
