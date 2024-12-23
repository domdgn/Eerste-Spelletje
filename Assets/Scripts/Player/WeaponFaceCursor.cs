using UnityEngine;

public class WeaponFaceCursor : MonoBehaviour
{
    private Vector3 lookDirection = Vector3.zero;
    Camera mainCamera;
    private PlayerMovement playerMovement;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
 
        mainCamera = Camera.main;
    }
    void faceCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        lookDirection = mousePos - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(-90,0,0);
    }
    void Update()
    {
        if (playerMovement.isMovementBlocked) return;
        faceCursor();
    }
}
