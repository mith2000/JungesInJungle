using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageGate : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    public void ExitStage()
    {
        Debug.Log("Stage done");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameMaster.GetInstance().SavePlayerInfo(player.GetComponent<PlayerInfo>());

        sceneLoader.LoadNextScene();
    }
}
