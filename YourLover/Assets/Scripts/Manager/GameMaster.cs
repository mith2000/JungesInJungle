using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    [Header ("Save Player Data")]
    public CharacterRole playerRole;
    public PlayerStatSaver playerStat = new PlayerStatSaver();
    public int maxCrystalPlayerUse = 4;

    public Stages currentStage;

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

        playerStat.currentCoin = PlayerPrefs.GetInt("PlayerCoins");
        Debug.Log("Game data loaded");
    }

    public static GameMaster GetInstance()
    {
        //if (instance == null) instance = new GameMaster();
        return instance;
    }

    public void StartGame()
    {
        SceneLoader sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

        StartCoroutine(sceneLoader.StartGame());
        Debug.Log("Game ON !!");

        //AudioManager.GetInstance().Stop("LobbySong");
        AudioManager.GetInstance().Stop("TitleSong");
        AudioManager.GetInstance().Play("SceneTransit");
        AudioManager.GetInstance().Play("GameBackgroundMusic", false);
    }

    public Stages WhatCurrentStage()
    {
        switch (SceneManager.GetActiveScene().buildIndex + 1)
        {
            case 2:
                currentStage = Stages.Stage_1_1;
                break;
            case 3:
                currentStage = Stages.Stage_1_2;
                break;
            case 4:
                currentStage = Stages.Stage_1_3;
                break;
            case 5:
                currentStage = Stages.Stage_1_4;
                break;
            case 6:
                currentStage = Stages.Stage_1_5;
                break;
            case 7:
                currentStage = Stages.Stage_2_1;
                break;
            case 8:
                currentStage = Stages.Stage_2_2;
                break;
            case 9:
                currentStage = Stages.Stage_2_3;
                break;
            case 10:
                currentStage = Stages.Stage_2_4;
                break;
            case 11:
                currentStage = Stages.Stage_2_5;
                break;
            case 12:
                currentStage = Stages.Stage_3_1;
                break;
            case 13:
                currentStage = Stages.Stage_3_2;
                break;
            case 14:
                currentStage = Stages.Stage_3_3;
                break;
            case 15:
                currentStage = Stages.Stage_3_4;
                break;
            case 16:
                currentStage = Stages.Stage_3_5;
                break;
            default:
                break;
        }
        Debug.Log("Stage Checked");
        return currentStage;
    }

    public void SavePlayerInfo(PlayerInfo info)
    {
        playerStat.maxHealth = info.maxHealth;
        playerStat.currentHealth = info.currentHealth;
        playerStat.maxArmor = info.maxArmor;
        playerStat.delayRegenEnergy = info.delayRegenEnergy;
        playerStat.currentCoin = info.currentCoin;
        playerStat.firstTimeHealthPotion = info.firstTimeHealthPotion;
        playerStat.firstTimeHealthCrystal = info.firstTimeHealthCrystal;
        playerStat.firstTimeArmorCrystal = info.firstTimeArmorCrystal;
        playerStat.firstTimeAttackSpeedCrystal = info.firstTimeAttackSpeedCrystal;
        playerStat.firstTimeRainbowCrystal = info.firstTimeRainbowCrystal;
    }

    public enum CharacterRole
    {
        Monkey = 1,
        Cat = 2
    }

    public enum Stages
    {
        Stage_1_1 = 1,
        Stage_1_2 = 2,
        Stage_1_3 = 3,
        Stage_1_4 = 4,
        Stage_1_5 = 5,
        Stage_2_1 = 6,
        Stage_2_2 = 7,
        Stage_2_3 = 8,
        Stage_2_4 = 9,
        Stage_2_5 = 10,
        Stage_3_1 = 11,
        Stage_3_2 = 12,
        Stage_3_3 = 13,
        Stage_3_4 = 14,
        Stage_3_5 = 15
    }

    public class PlayerStatSaver
    {
        public int maxHealth = 0;
        public int currentHealth = 0;
        public int maxArmor = 0;
        public float delayRegenEnergy = 0;

        public int useCrystalTime = 0;

        public int currentCoin = 0;

        public bool firstTimeHealthPotion = true;
        public bool firstTimeHealthCrystal = true;
        public bool firstTimeArmorCrystal = true;
        public bool firstTimeAttackSpeedCrystal = true;
        public bool firstTimeRainbowCrystal = true;
    }
}
