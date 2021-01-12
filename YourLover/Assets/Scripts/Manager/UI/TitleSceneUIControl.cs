using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneUIControl : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumn");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolumn");
    }

    public void Setting_OnClick()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSetting_OnClick()
    {
        settingPanel.SetActive(false);
    }

    public void MusicSlider_ValueChange()
    {
        AudioManager.GetInstance().musicVolumn = musicSlider.value;
        AudioManager.GetInstance().MusicVolumn_OnChange();

        PlayerPrefs.SetFloat("MusicVolumn", AudioManager.GetInstance().musicVolumn);
    }

    public void SFXSlider_ValueChange()
    {
        AudioManager.GetInstance().SFXVolumn = SFXSlider.value;
        AudioManager.GetInstance().SFXVolumn_OnChange();

        PlayerPrefs.SetFloat("SFXVolumn", AudioManager.GetInstance().SFXVolumn);
    }
}
