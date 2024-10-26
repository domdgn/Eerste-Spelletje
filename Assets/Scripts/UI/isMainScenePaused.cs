using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool isMainScenePaused = false;
    private PlayerMovement movement;
    public string[] additiveScenes;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            movement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        bool additiveSceneLoaded = false;

        foreach (string sceneName in additiveScenes)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                additiveSceneLoaded = true;
                break;
            }
        }

        if (additiveSceneLoaded && !isMainScenePaused)
        {
            PauseMainScene();
        }

        if (!additiveSceneLoaded && isMainScenePaused)
        {
            ResumeMainScene();
        }
    }

    void PauseMainScene()
    {
        if (movement != null)
        {
            movement.BlockPlayerMovement(true);
        }
        Time.timeScale = 0f; // Pause the main scene
        isMainScenePaused = true;
    }

    void ResumeMainScene()
    {
        if (movement != null)
        {
            movement.BlockPlayerMovement(false);
        }
        Time.timeScale = 1f; // Resume the main scene
        isMainScenePaused = false;
    }
}
