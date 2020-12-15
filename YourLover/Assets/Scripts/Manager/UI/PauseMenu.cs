using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePause = false;

    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject pauseButton;
    private GameObject moveJoystick;
    private GameObject aimJoystick;
    private GameObject[] skillButtons;

    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePause)
            {
                ResumeGame_OnClick();
            }
            else
            {
                PauseGame_OnClick();
            }
        }
    }

    public void PauseGame_OnClick()
    {
        UI.SetActive(true);
        pauseButton.SetActive(false);
        moveJoystick = GameObject.FindGameObjectWithTag("MoveJoystick");
        moveJoystick.SetActive(false);
        aimJoystick = GameObject.FindGameObjectWithTag("AimJoystick");
        aimJoystick.SetActive(false);
        skillButtons = GameObject.FindGameObjectsWithTag("SkillButton");
        foreach (GameObject button in skillButtons)
        {
            button.SetActive(false);
        }
        Time.timeScale = 0f;
        gamePause = true;
    }

    public void ResumeGame_OnClick()
    {
        //FindObjectOfType<AudioManager>().Play("Click", true);
        UI.SetActive(false);
        pauseButton.SetActive(true);
        moveJoystick.SetActive(true);
        aimJoystick.SetActive(true);
        foreach (GameObject button in skillButtons)
        {
            button.SetActive(true);
        }
        Time.timeScale = 1f;
        gamePause = false;
    }

    public void QuitGame_OnClick()
    {
        Time.timeScale = 1f;
        StartCoroutine(sceneLoader.LoadScene(0));
    }
}
