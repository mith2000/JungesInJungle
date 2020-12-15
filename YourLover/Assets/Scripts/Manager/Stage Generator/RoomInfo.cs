using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField]
    private bool specialRoom = false;   //entry room
    [SerializeField]
    private bool hasDoorTop = false;
    [SerializeField]
    private bool hasDoorBottom = false;
    [SerializeField]
    private bool hasDoorLeft = false;
    [SerializeField]
    private bool hasDoorRight = false;
    [SerializeField]
    private GameObject doorTop;
    [SerializeField]
    private GameObject doorBottom;
    [SerializeField]
    private GameObject doorLeft;
    [SerializeField]
    private GameObject doorRight;

    [SerializeField]
    private bool isBossRoom = false;
    [SerializeField]
    private GameObject forestBossPrefab;
    [SerializeField]
    private GameObject sandBossPrefab;
    [SerializeField]
    private GameObject urbanBossPrefab;
    [SerializeField]
    private SpawnScripts scripts;
    [SerializeField]
    private GameObject portal;
    [SerializeField]
    private GameObject roomChest;
    [SerializeField]
    private GameObject bossRoomMinimapIcon;

    private GameObject spawner;
    private GameObject boss;
    private bool isStartFightBoss = false;

    private GameObject minimap;

    private bool closed = false;

    private void Awake()
    {
        if (!specialRoom)
        {
            if (hasDoorTop)
                doorTop.SetActive(true);
            if (hasDoorBottom)
                doorBottom.SetActive(true);
            if (hasDoorLeft)
                doorLeft.SetActive(true);
            if (hasDoorRight)
                doorRight.SetActive(true);
        }
        if (isBossRoom)
            Instantiate(bossRoomMinimapIcon, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (closed || specialRoom) return;

        if (collision.CompareTag("Player"))
        {
            #region Close doors
            if (hasDoorTop)
            {
                doorTop.GetComponent<Animator>().SetTrigger("Close");
            }
            if (hasDoorBottom)
            {
                doorBottom.GetComponent<Animator>().SetTrigger("Close");
            }
            if (hasDoorLeft)
            {
                doorLeft.GetComponent<Animator>().SetTrigger("Close");
            }
            if (hasDoorRight)
            {
                doorRight.GetComponent<Animator>().SetTrigger("Close");
            }
            closed = true;
            #endregion

            minimap = GameObject.FindGameObjectWithTag("Minimap");
            minimap.SetActive(false);

            if (!isBossRoom)
                GenerateWaveSpawner();
            else
                GenerateBoss();
        }
    }

    private void GenerateBoss()
    {
        if (GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_1_5)
        {
            if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
            {
                boss = Instantiate(forestBossPrefab, transform.position, Quaternion.identity);
            }
            else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
            {
                boss = Instantiate(sandBossPrefab, transform.position, Quaternion.identity);
            }
        }
        else if (GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_2_5)
        {
            if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
            {
                boss = Instantiate(urbanBossPrefab, transform.position, Quaternion.identity);
            }
        }
        isStartFightBoss = true;
    }

    private void BossSlayed()
    {
        isStartFightBoss = false;
        Instantiate(portal, transform.position, Quaternion.identity);

        OpenDoors();
    }

    private void GenerateWaveSpawner()
    {
        switch (GameMaster.GetInstance().currentStage)
        {
            case GameMaster.Stages.Stage_1_1:
                RandomScriptStage1Level1();
                break;
            case GameMaster.Stages.Stage_1_2:
                RandomScriptStage1Level1();
                break;
            case GameMaster.Stages.Stage_1_3:
                RandomScriptStage1Level2();
                break;
            case GameMaster.Stages.Stage_1_4:
                RandomScriptStage1Level2();
                break;
            case GameMaster.Stages.Stage_1_5:
                RandomScriptStage1Level3();
                break;
            case GameMaster.Stages.Stage_2_1:
                RandomScriptStage2Level1();
                break;
            case GameMaster.Stages.Stage_2_2:
                RandomScriptStage2Level2();
                break;
            case GameMaster.Stages.Stage_2_3:
                RandomScriptStage2Level2();
                break;
            case GameMaster.Stages.Stage_2_4:
                RandomScriptStage2Level3();
                break;
            case GameMaster.Stages.Stage_2_5:
                RandomScriptStage2Level3();
                break;
            case GameMaster.Stages.Stage_3_1:
                break;
            case GameMaster.Stages.Stage_3_2:
                break;
            case GameMaster.Stages.Stage_3_3:
                break;
            case GameMaster.Stages.Stage_3_4:
                break;
            case GameMaster.Stages.Stage_3_5:
                break;
            default:
                break;
        }
    }

    #region Script Spawner
    void RandomScriptStage1Level1()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, scripts.forestLv1Scripts.Length);
            spawner = Instantiate(scripts.forestLv1Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, scripts.sandLv1Scripts.Length);
            spawner = Instantiate(scripts.sandLv1Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage1Level2()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, scripts.forestLv2Scripts.Length);
            spawner = Instantiate(scripts.forestLv2Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, scripts.sandLv2Scripts.Length);
            spawner = Instantiate(scripts.sandLv2Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage1Level3()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, scripts.forestLv3Scripts.Length);
            spawner = Instantiate(scripts.forestLv3Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, scripts.sandLv3Scripts.Length);
            spawner = Instantiate(scripts.sandLv3Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level1()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, scripts.urbanLv1Scripts.Length);
            spawner = Instantiate(scripts.urbanLv1Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level2()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, scripts.urbanLv2Scripts.Length);
            spawner = Instantiate(scripts.urbanLv2Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level3()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, scripts.urbanLv3Scripts.Length);
            spawner = Instantiate(scripts.urbanLv3Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    #endregion

    private void Update()
    {
        if (spawner != null)
        {
            if (spawner.GetComponent<WaveSpawner>().finishedWave)
            {
                RoomHasDone();
                Destroy(spawner);
            }
        }

        if (isBossRoom)
        {
            if (isStartFightBoss)
            {
                if (boss == null)   //isSFB make sure boss != null, if after that boss is null -> it dead
                {
                    BossSlayed();
                }
            }
        }
    }

    private void RoomHasDone()
    {
        Instantiate(roomChest, transform.position, Quaternion.identity);

        OpenDoors();
    }

    private void OpenDoors()
    {
        if (hasDoorTop)
        {
            doorTop.GetComponent<Animator>().SetTrigger("Open");
        }
        if (hasDoorBottom)
        {
            doorBottom.GetComponent<Animator>().SetTrigger("Open");
        }
        if (hasDoorLeft)
        {
            doorLeft.GetComponent<Animator>().SetTrigger("Open");
        }
        if (hasDoorRight)
        {
            doorRight.GetComponent<Animator>().SetTrigger("Open");
        }
        minimap.SetActive(true);
    }
}
