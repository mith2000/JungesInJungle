using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] float transitionTime = 1f;

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public IEnumerator LoadScene(int levelIndex)
    {
        AudioManager.GetInstance().Play("SceneTransit");
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        Debug.Log("Checking What Stage ... ");
        GameMaster.GetInstance().WhatCurrentStage();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnterLobby()
    {
        StartCoroutine(LoadLobbyScene());
    }

    IEnumerator LoadLobbyScene()
    {
        AudioManager.GetInstance().Play("SceneTransit");
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator StartGame()
    {
        anim.SetTrigger("Load");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(2);
        Debug.Log("Checking What Stage ... ");
        GameMaster.GetInstance().WhatCurrentStage();
    }

    public void BackToLobby_OnClick()
    {
        AudioManager.GetInstance().Stop("EndSong");
        AudioManager.GetInstance().Play("TitleSong", false);
        StartCoroutine(LoadScene(1));
    }
}
