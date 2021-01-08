using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField] bool specialRoom = false;   //entry room

    [Header ("Door Settings")]
    [SerializeField] bool hasDoorTop = false;
    [SerializeField] bool hasDoorBottom = false;
    [SerializeField] bool hasDoorLeft = false;
    [SerializeField] bool hasDoorRight = false;
    [SerializeField] GameObject doorTop;
    [SerializeField] GameObject doorBottom;
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;

    [Header ("Boss Settings")]
    [SerializeField] bool isBossRoom = false;
    [SerializeField] GameObject forestBossPrefab;
    [SerializeField] GameObject sandBossPrefab;
    [SerializeField] GameObject urbanBossPrefab;

    [Header ("Spawn Reference")]
    [SerializeField] SpawnScripts enemySpawnScript;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject roomChest;
    [SerializeField] GameObject bossRoomMinimapIcon;
    [SerializeField] ObstacleScripts obstacleScript;

    GameObject spawner;
    GameObject boss;
    bool isStartFightBoss = false;

    GameObject minimap;

    bool closed = false;

    private void Awake()
    {
        //Close all doors except first room
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

        //If boss room, instantiate boss room minimap icon
        if (isBossRoom)
            Instantiate(bossRoomMinimapIcon, transform.position, Quaternion.identity);

        //If not first room and boss room, instantiate obstacles
        if (!specialRoom)
        {
            if (!isBossRoom)
            {
                GenerateObstacle();
            }
            else
            {
                RandomObstacleBossRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (closed || specialRoom) return;

        if (collision.CompareTag("Player"))
        {
            AudioManager.GetInstance().Play("CloseDoor");

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
        AudioManager.GetInstance().Stop("GameBackgroundMusic");
        AudioManager.GetInstance().Play("FightBoss", false);
        if (GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_1_5)
        {
            if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
            {
                boss = Instantiate(forestBossPrefab, transform.position, Quaternion.identity);

                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.forestBossSentences);
                DialogSystem.GetInstance().StartText();
            }
            else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
            {
                boss = Instantiate(sandBossPrefab, transform.position, Quaternion.identity);

                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.sandBossSentences);
                DialogSystem.GetInstance().StartText();
            }
        }
        else if (GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_2_5)
        {
            if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
            {
                boss = Instantiate(urbanBossPrefab, transform.position, Quaternion.identity);

                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.urbanBossSentences);
                DialogSystem.GetInstance().StartText();
            }
        }
        isStartFightBoss = true;
    }

    private void BossSlayed()
    {
        AudioManager.GetInstance().Stop("FightBoss");
        AudioManager.GetInstance().Play("EndBoss");
        AudioManager.GetInstance().Play("GameBackgroundMusic", false);
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

    private void GenerateObstacle()
    {
        switch (GameMaster.GetInstance().currentStage)
        {
            case GameMaster.Stages.Stage_1_1:
                RandomObstacleStage1();
                break;
            case GameMaster.Stages.Stage_1_2:
                RandomObstacleStage1();
                break;
            case GameMaster.Stages.Stage_1_3:
                RandomObstacleStage1();
                break;
            case GameMaster.Stages.Stage_1_4:
                RandomObstacleStage1();
                break;
            case GameMaster.Stages.Stage_1_5:
                RandomObstacleStage1();
                break;
            case GameMaster.Stages.Stage_2_1:
                RandomObstacleStage2();
                break;
            case GameMaster.Stages.Stage_2_2:
                RandomObstacleStage2();
                break;
            case GameMaster.Stages.Stage_2_3:
                RandomObstacleStage2();
                break;
            case GameMaster.Stages.Stage_2_4:
                RandomObstacleStage2();
                break;
            case GameMaster.Stages.Stage_2_5:
                RandomObstacleStage2();
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
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.forestLv1Scripts.Length);
            spawner = Instantiate(enemySpawnScript.forestLv1Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.sandLv1Scripts.Length);
            spawner = Instantiate(enemySpawnScript.sandLv1Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage1Level2()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.forestLv2Scripts.Length);
            spawner = Instantiate(enemySpawnScript.forestLv2Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.sandLv2Scripts.Length);
            spawner = Instantiate(enemySpawnScript.sandLv2Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage1Level3()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.forestLv3Scripts.Length);
            spawner = Instantiate(enemySpawnScript.forestLv3Scripts[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.sandLv3Scripts.Length);
            spawner = Instantiate(enemySpawnScript.sandLv3Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level1()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.urbanLv1Scripts.Length);
            spawner = Instantiate(enemySpawnScript.urbanLv1Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level2()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.urbanLv2Scripts.Length);
            spawner = Instantiate(enemySpawnScript.urbanLv2Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    void RandomScriptStage2Level3()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, enemySpawnScript.urbanLv3Scripts.Length);
            spawner = Instantiate(enemySpawnScript.urbanLv3Scripts[rand], transform.position, Quaternion.identity);
        }
    }
    #endregion

    #region Obstacle Instantiater
    private void RandomObstacleStage1()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            int rand = UnityEngine.Random.Range(0, obstacleScript.forestObstacleGroups.Length);
            Instantiate(obstacleScript.forestObstacleGroups[rand], transform.position, Quaternion.identity);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            int rand = UnityEngine.Random.Range(0, obstacleScript.sandObstacleGroups.Length);
            Instantiate(obstacleScript.sandObstacleGroups[rand], transform.position, Quaternion.identity);
        }
    }
    private void RandomObstacleStage2()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            int rand = UnityEngine.Random.Range(0, obstacleScript.urbanObstacleGroups.Length);
            Instantiate(obstacleScript.urbanObstacleGroups[rand], transform.position, Quaternion.identity);
        }
    }
    private void RandomObstacleBossRoom()
    {
        if (GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_1_5)
        {
            if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
            {
                Instantiate(obstacleScript.forestBossOG, transform.position, Quaternion.identity);
            }
            else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
            {
                Instantiate(obstacleScript.sandBossOG, transform.position, Quaternion.identity);
            }
        }
        else if(GameMaster.GetInstance().currentStage == GameMaster.Stages.Stage_2_5)
        {
            if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
            {
                Instantiate(obstacleScript.urbanBossOG, transform.position, Quaternion.identity);
            }
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
        Destroy(gameObject);
    }
}
