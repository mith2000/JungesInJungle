using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapCameraFollow : MonoBehaviour
{
    Transform playerTransform;
    float speed = 1;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
            if (GameObject.FindGameObjectWithTag("Player") != null)
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform != null)
            transform.position = playerTransform.position;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.Lerp(transform.position, playerTransform.position, speed);
        }
    }
}
