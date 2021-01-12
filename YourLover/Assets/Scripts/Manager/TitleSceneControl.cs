using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneControl : MonoBehaviour
{
    void Start()
    {
        AudioManager.GetInstance().Play("TitleSong", false);

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetFloat("MusicVolumn", 0.5f);
        //PlayerPrefs.SetFloat("SFXVolumn", 0.75f);
    }
}
