using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private HealthSystem healthSystem;
    RectTransform rectTransform;
    Camera mainCamera;

    void Start()
    {
        gameObject.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure the player is tagged as "Player"
        if (player != null)
        {
            healthSystem = player.GetComponent<HealthSystem>();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (healthSystem != null && healthSystem.isPlayerDead == false)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 viewportPoint = mainCamera.ScreenToViewportPoint(mousePos);
            rectTransform.anchorMin = viewportPoint;
            rectTransform.anchorMax = viewportPoint;
            
        }

        else
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }
    }
}
