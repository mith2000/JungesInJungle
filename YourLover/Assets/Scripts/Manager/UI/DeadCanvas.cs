using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCanvas : MonoBehaviour
{
    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    public void QuitGame_OnClick()
    {
        AudioManager.GetInstance().Stop("GameBackgroundMusic");
        Time.timeScale = 1f;
        StartCoroutine(sceneLoader.LoadScene(0));
    }
}
