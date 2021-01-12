using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI guildText;

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

        StartCoroutine(ChangeGuildText());
    }

    IEnumerator ChangeGuildText()
    {
        for (int i = 0; i < 10; i++)
        {
            int rand = Random.Range(0, guild.Length);
            guildText.text = guild[rand];
            yield return new WaitForSeconds(5);
            i++;
        }
    }

    public void BackToLobby_OnClick()
    {
        AudioManager.GetInstance().Stop("EndSong");
        AudioManager.GetInstance().Play("TitleSong", false);
        StartCoroutine(LoadScene(1));
    }

    string[] guild = { 
        "Guild / Tips:\nUse your left joystick to control your character movement.",
        "Guild / Tips:\nYour right joystick is used for aiming.",
        "Guild / Tips:\nAiming brings greater accuracy.",
        "Guild / Tips:\nYour health wont regen over time, but your armor does.",
        "Guild / Tips:\nWhen out of armor, you are vulnerable.",
        "Guild / Tips:\nMonKee is very mischievous. His mom always followed to take care of him.",
        "Guild / Tips:\nMonKee is good at calling his mom out to protect him when he gets bullied.",
        "Guild / Tips:\nKat's mag is larger than MonKee's. She has better preparation for battles.",
        "Guild / Tips:\nKat's special skill can slowdown the time except herself. It also reverse enemy's bullet direction.",
        "Guild / Tips:\nHealth potion give you 2 HP per use.",
        "Guild / Tips:\nCrystals are great for stat boosting. But not all are like that.",
        "Guild / Tips:\nUse minimap to determind where we are going.",
        "Guild / Tips:\nBreak the gold mine and you will look like Johny Dang. Shiny shiny !!"
    };
}
