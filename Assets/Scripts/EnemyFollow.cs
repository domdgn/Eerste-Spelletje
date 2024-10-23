using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private float followSpeed = 7.5f;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
    }
}
