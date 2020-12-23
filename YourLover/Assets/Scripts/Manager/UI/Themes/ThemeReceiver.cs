using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeReceiver : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] blockSprites;

    [SerializeField] SpriteRenderer[] grounds;

    private void Awake()
    {
        ReceiveTheme();
    }

    private void ReceiveTheme()
    {
        switch (GameMaster.GetInstance().currentStage)
        {
            case GameMaster.Stages.Stage_1_1:
                Stage1ChangeTheme();
                break;
            case GameMaster.Stages.Stage_1_2:
                Stage1ChangeTheme();
                break;
            case GameMaster.Stages.Stage_1_3:
                Stage1ChangeTheme();
                break;
            case GameMaster.Stages.Stage_1_4:
                Stage1ChangeTheme();
                break;
            case GameMaster.Stages.Stage_1_5:
                Stage1ChangeTheme();
                break;
            case GameMaster.Stages.Stage_2_1:
                Stage2ChangeTheme();
                break;
            case GameMaster.Stages.Stage_2_2:
                Stage2ChangeTheme();
                break;
            case GameMaster.Stages.Stage_2_3:
                Stage2ChangeTheme();
                break;
            case GameMaster.Stages.Stage_2_4:
                Stage2ChangeTheme();
                break;
            case GameMaster.Stages.Stage_2_5:
                Stage2ChangeTheme();
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

    private void Stage1ChangeTheme()
    {
        if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Forest)
        {
            GetThemeSprites(ThemeManager.GetInstance().foresetTheme);
        }
        else if (ThemeManager.GetInstance().stage1Theme == ThemeManager.Stage1Themes.Sand)
        {
            GetThemeSprites(ThemeManager.GetInstance().sandTheme);
        }
    }

    private void Stage2ChangeTheme()
    {
        if (ThemeManager.GetInstance().stage2Theme == ThemeManager.Stage2Themes.Urban)
        {
            GetThemeSprites(ThemeManager.GetInstance().urbanTheme);
        }
    }

    private void GetThemeSprites(Theme theme)
    {
        foreach (SpriteRenderer block in blockSprites)
        {
            switch (block.name)
            {
                case "1x5":
                    block.sprite = theme.block1x5;
                    break;
                case "1x9 T":
                    block.sprite = theme.block1x9T;
                    break;
                case "1x9":
                    block.sprite = theme.block1x9;
                    break;
                case "1x14":
                    block.sprite = theme.block1x14;
                    break;
                case "4x1":
                    block.sprite = theme.block4x1;
                    break;
                case "7.5x1 R":
                    block.sprite = theme.block75x1R;
                    break;
                case "7.5x1":
                    block.sprite = theme.block75x1;
                    break;
                case "9x1":
                    block.sprite = theme.block9x1;
                    break;
                case "12.5x1 R":
                    block.sprite = theme.block125x1R;
                    break;
                case "12.5x1":
                    block.sprite = theme.block125x1;
                    break;
                case "14x1":
                    block.sprite = theme.block14x1;
                    break;
                case "24x1":
                    block.sprite = theme.block24x1;
                    break;
                case "1x5 ":
                    block.sprite = theme.block1x5;
                    break;
                case "1x9 T ":
                    block.sprite = theme.block1x9T;
                    break;
                case "1x9 ":
                    block.sprite = theme.block1x9;
                    break;
                case "1x14 ":
                    block.sprite = theme.block1x14;
                    break;
                case "4x1 ":
                    block.sprite = theme.block4x1;
                    break;
                case "7.5x1 R ":
                    block.sprite = theme.block75x1R;
                    break;
                case "7.5x1 ":
                    block.sprite = theme.block75x1;
                    break;
                case "9x1 ":
                    block.sprite = theme.block9x1;
                    break;
                case "12.5x1 R ":
                    block.sprite = theme.block125x1R;
                    break;
                case "12.5x1 ":
                    block.sprite = theme.block125x1;
                    break;
                case "14x1 ":
                    block.sprite = theme.block14x1;
                    break;
                case "24x1 ":
                    block.sprite = theme.block24x1;
                    break;
                default:
                    break;
            }
        }

        foreach (SpriteRenderer ground in grounds)
        {
            switch (ground.name)
            {
                case "Big":
                    ground.sprite = theme.groundBig;
                    break;
                case "Small":
                    ground.sprite = theme.groundSmall;
                    break;
                case "Bridge":
                    ground.sprite = theme.groundBridge;
                    break;
                case "Bridge Lie":
                    ground.sprite = theme.groundBridgeLie;
                    break;
                case "Bridge Big":
                    ground.sprite = theme.groundBridgeBig;
                    break;
                case "Big ":
                    ground.sprite = theme.groundBig;
                    break;
                case "Small ":
                    ground.sprite = theme.groundSmall;
                    break;
                case "Bridge ":
                    ground.sprite = theme.groundBridge;
                    break;
                case "Bridge Lie ":
                    ground.sprite = theme.groundBridgeLie;
                    break;
                case "Bridge Big ":
                    ground.sprite = theme.groundBridgeBig;
                    break;
                default:
                    break;
            }
        }
    }
}
