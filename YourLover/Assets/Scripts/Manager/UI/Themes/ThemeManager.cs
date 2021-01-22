using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    static ThemeManager instance;

    public Stage1Themes stage1Theme;
    public Stage2Themes stage2Theme;
    public Stage3Themes stage3Theme;

    public Theme foresetTheme;
    public Theme sandTheme;
    public Theme urbanTheme;

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

        GenerateTheme();
    }

    public static ThemeManager GetInstance()
    {
        //if (instance == null) instance = new GameMaster();
        return instance;
    }

    private void GenerateTheme()
    {
        int rand1 = Random.Range(0, 100);
        if (rand1 < 65)
            stage1Theme = Stage1Themes.Forest;
        else
            stage1Theme = Stage1Themes.Sand;

        stage2Theme = Stage2Themes.Urban;

        int rand3 = Random.Range(0, 100);
        if (rand3 < 50)
            stage3Theme = Stage3Themes.DeepForest;
        else
            stage3Theme = Stage3Themes.Mountain;
    }

    public enum Stage1Themes
    {
        Forest = 0,
        Sand = 1
    }

    public enum Stage2Themes
    {
        Urban = 0
    }

    //Khi lam stage 3 thi nho chinh Pathfinder lai cho Level2 + Level3 + LevelBoss
    //Obstacle layer mask = wall + obstacle
    //Hien tai cac obstacle dang co layer la wall do luoi` chinh
    public enum Stage3Themes
    {
        DeepForest = 0,
        Mountain = 1
    }
}
