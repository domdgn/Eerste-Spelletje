using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 12.5f;
    public float stopDistance = 0.1f;
    public float padding = 1f;
    private Vector3 lookDirection = Vector3.zero;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float movement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        clampMovement();
        faceCursor();

        if (ButtonScript.CursorMovement)
        {
            if (Vector3.Distance(transform.position, lookDirection + transform.position) > stopDistance)
            {
                transform.Translate(strafe, 0, movement);
            }
        }
        else
        {
            transform.Translate(Vector3.right * strafe, Space.World);
            transform.Translate(Vector3.forward * movement, Space.World);
        }
    }

    void faceCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        lookDirection = mousePos - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    void clampMovement()
    {
        Vector3 position = transform.position;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float xMin = mainCamera.transform.position.x - cameraWidth + padding;
        float xMax = mainCamera.transform.position.x + cameraWidth - padding;
        float zMin = mainCamera.transform.position.z - cameraHeight + padding;
        float zMax = mainCamera.transform.position.z + cameraHeight - padding;

        // Clamp the player's position within the screen bounds
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.z = Mathf.Clamp(position.z, zMin, zMax);

        // Update the player's position
        transform.position = position;
    }
}
