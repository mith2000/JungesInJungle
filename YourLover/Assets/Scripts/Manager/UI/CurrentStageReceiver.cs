using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CurrentStageReceiver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText; 

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            StartCoroutine(DelayGetStage());
        }
    }

    IEnumerator DelayGetStage()
    {
        yield return new WaitForSeconds(.6f);
        switch (GameMaster.GetInstance().currentStage)
        {
            case GameMaster.Stages.Stage_1_1:
                stageText.text = "Stage 1-1";
                break;
            case GameMaster.Stages.Stage_1_2:
                stageText.text = "Stage 1-2";
                break;
            case GameMaster.Stages.Stage_1_3:
                stageText.text = "Stage 1-3";
                break;
            case GameMaster.Stages.Stage_1_4:
                stageText.text = "Stage 1-4";
                break;
            case GameMaster.Stages.Stage_1_5:
                stageText.text = "Stage 1-5";
                break;
            case GameMaster.Stages.Stage_2_1:
                stageText.text = "Stage 2-1";
                break;
            case GameMaster.Stages.Stage_2_2:
                stageText.text = "Stage 2-2";
                break;
            case GameMaster.Stages.Stage_2_3:
                stageText.text = "Stage 2-3";
                break;
            case GameMaster.Stages.Stage_2_4:
                stageText.text = "Stage 2-4";
                break;
            case GameMaster.Stages.Stage_2_5:
                stageText.text = "Stage 2-5";
                break;
            case GameMaster.Stages.Stage_3_1:
                stageText.text = "Stage 3-1";
                break;
            case GameMaster.Stages.Stage_3_2:
                stageText.text = "Stage 3-2";
                break;
            case GameMaster.Stages.Stage_3_3:
                stageText.text = "Stage 3-3";
                break;
            case GameMaster.Stages.Stage_3_4:
                stageText.text = "Stage 3-4";
                break;
            case GameMaster.Stages.Stage_3_5:
                stageText.text = "Stage 3-5";
                break;
            default:
                stageText.text = " - ";
                break;
        }
    }
}
