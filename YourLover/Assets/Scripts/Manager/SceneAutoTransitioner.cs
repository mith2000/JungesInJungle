using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoTransitioner : MonoBehaviour
{
    float transitionTime = 3f;
    SceneLoader sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(transitionTime);
        sceneLoader.LoadNextScene();
    }
}
