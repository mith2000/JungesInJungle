//Link: https://www.youtube.com/watch?v=0VGosgaoTsw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;

    float slowdownFactor = 0.2f;
    float slowdownLenght = 3f;

    bool isSlowed = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static TimeManager GetInstance()
    {
        //if (instance == null) instance = new GameMaster();
        return instance;
    }

    public float Factor
    {
        get { return slowdownFactor; }
    }

    //void Update()
    //{
    //    //Its no problem the last time I test with Kat's skill
    //    //if (!PauseMenu.gamePause)
    //    //{
    //    //    Time.timeScale += (1 / slowdownLenght) * Time.unscaledDeltaTime;
    //    //    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    //    //}
    //}

    public void DoSlowmo(float lenght)
    {
        if (isSlowed) return;

        isSlowed = true;
        slowdownLenght = lenght;
        float saveTimeScale = Time.timeScale;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(GiveBackTime(saveTimeScale));
    }

    IEnumerator GiveBackTime(float timeScaleBack)
    {
        yield return new WaitForSeconds(slowdownLenght);
        Time.timeScale = timeScaleBack;
        isSlowed = false;
    }
}
