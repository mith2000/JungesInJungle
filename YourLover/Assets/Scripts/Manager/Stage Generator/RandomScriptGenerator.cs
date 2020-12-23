using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScriptGenerator : MonoBehaviour
{
    [SerializeField] StageScripts scripts;

    int lv1Quantity;
    int lv2Quantity;
    int lv3Quantity;
    int bossQuantity;

    void Start()
    {
        lv1Quantity = scripts.level1Scripts.Length;
        lv2Quantity = scripts.level2Scripts.Length;
        lv3Quantity = scripts.level3Scripts.Length;
        bossQuantity = scripts.bossScripts.Length;

        Debug.Log("Ready for generate stage...");
        StartCoroutine(GenerateStage());
    }

    IEnumerator GenerateStage()
    {
        yield return new WaitForSeconds(.2f);

        switch (GameMaster.GetInstance().currentStage)
        {
            case GameMaster.Stages.Stage_1_1:
                RandomScript1and2();
                break;
            case GameMaster.Stages.Stage_1_2:
                RandomScript1and2();
                break;
            case GameMaster.Stages.Stage_1_3:
                RandomScript2();
                break;
            case GameMaster.Stages.Stage_1_4:
                RandomScript2();
                break;
            case GameMaster.Stages.Stage_1_5:
                RandomScriptBoss();
                break;
            case GameMaster.Stages.Stage_2_1:
                RandomScript2();
                break;
            case GameMaster.Stages.Stage_2_2:
                RandomScript2();
                break;
            case GameMaster.Stages.Stage_2_3:
                RandomScript2and3();
                break;
            case GameMaster.Stages.Stage_2_4:
                RandomScript2and3();
                break;
            case GameMaster.Stages.Stage_2_5:
                RandomScriptBoss();
                break;
            case GameMaster.Stages.Stage_3_1:
                RandomScript2and3();
                break;
            case GameMaster.Stages.Stage_3_2:
                RandomScript2and3();
                break;
            case GameMaster.Stages.Stage_3_3:
                RandomScript2and3();
                break;
            case GameMaster.Stages.Stage_3_4:
                RandomScript3();
                break;
            case GameMaster.Stages.Stage_3_5:
                RandomScriptBoss();
                break;
            default:
                Debug.LogError("Falsed to choose dungeon");
                //GameMaster.GetInstance().WhatCurrentStage();
                //GenerateStage();
                break;
        }
    }

    private void RandomScript1and2()
    {
        if (Random.Range(0, lv1Quantity + lv2Quantity) < lv1Quantity)
        {
            int rand = Random.Range(0, lv1Quantity);
            Instantiate(scripts.level1Scripts[rand], transform.position, Quaternion.identity);
            Debug.Log("Selected Level " + scripts.level1Scripts[rand].name);
        }
        else
        {
            int rand = Random.Range(0, lv2Quantity);
            Instantiate(scripts.level2Scripts[rand], transform.position, Quaternion.identity);
            Debug.Log("Selected Level " + scripts.level2Scripts[rand].name);
        }
    }

    private void RandomScript2and3()
    {
        if (Random.Range(0, lv2Quantity + lv3Quantity) < lv2Quantity)
        {
            int rand = Random.Range(0, lv2Quantity);
            Instantiate(scripts.level2Scripts[rand], transform.position, Quaternion.identity);
            Debug.Log("Selected Level " + scripts.level2Scripts[rand].name);
        }
        else
        {
            int rand = Random.Range(0, lv3Quantity);
            Instantiate(scripts.level3Scripts[rand], transform.position, Quaternion.identity);
            Debug.Log("Selected Level " + scripts.level3Scripts[rand].name);
        }
    }

    private void RandomScript2()
    {
        int rand = Random.Range(0, lv2Quantity);
        Instantiate(scripts.level2Scripts[rand], transform.position, Quaternion.identity);
        Debug.Log("Selected Level " + scripts.level2Scripts[rand].name);
    }

    private void RandomScript3()
    {
        int rand = Random.Range(0, lv3Quantity);
        Instantiate(scripts.level3Scripts[rand], transform.position, Quaternion.identity);
        Debug.Log("Selected Level " + scripts.level3Scripts[rand].name);
    }

    private void RandomScriptBoss()
    {
        int rand = Random.Range(0, bossQuantity);
        Instantiate(scripts.bossScripts[rand], transform.position, Quaternion.identity);
        Debug.Log("Selected Level " + scripts.bossScripts[rand].name);
    }
}
