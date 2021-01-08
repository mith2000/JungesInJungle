using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject pauseButton;
    GameObject playerController;

    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    public void PauseGame_OnClick()
    {
        UI.SetActive(true);
        pauseButton.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player");
        playerController.GetComponent<PlayerController>().UnableControl();
        Time.timeScale = 0f;
        AudioManager.GetInstance().Pause("GameBackgroundMusic");
    }

    public void ResumeGame_OnClick()
    {
        UI.SetActive(false);
        pauseButton.SetActive(true);
        playerController.GetComponent<PlayerController>().EnableControl();
        Time.timeScale = 1f;
        AudioManager.GetInstance().UnPause("GameBackgroundMusic");
    }

    public void QuitGame_OnClick()
    {
        AudioManager.GetInstance().Play("Click");
        AudioManager.GetInstance().Stop("GameBackgroundMusic");
        Time.timeScale = 1f;
        StartCoroutine(sceneLoader.LoadScene(0));
    }
}
