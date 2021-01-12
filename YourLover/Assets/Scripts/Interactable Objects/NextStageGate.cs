using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageGate : MonoBehaviour
{
    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ExitStage();
        }
    }
#endif

    public void ExitStage()
    {
        Debug.Log("Stage done");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameMaster.GetInstance().SavePlayerInfo(player.GetComponent<PlayerInfo>());

        PlayerPrefs.SetInt("PlayerCoins", GameMaster.GetInstance().playerStat.currentCoin);
        PlayerPrefs.Save();
        Debug.Log("Saved player data");

        sceneLoader.LoadNextScene();
    }
}
