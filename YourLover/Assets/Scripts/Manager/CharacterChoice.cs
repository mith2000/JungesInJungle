using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoice : MonoBehaviour
{
    //Select of choicer only for Lobby Scene
    [HideInInspector] public GameMaster.CharacterRole selectedRole;

    public GameObject chooseCharacterCanvas;
    public GameObject pauseCanvas;

    public void ChooseMonkey_OnClick()
    {
        selectedRole = GameMaster.CharacterRole.Monkey;
        //For saving the choice for whole game
        GameMaster.GetInstance().playerRole = selectedRole;
        AfterChooseAction();
    }

    public void ChooseCat_OnClick()
    {
        selectedRole = GameMaster.CharacterRole.Cat;
        //For saving the choice for whole game
        GameMaster.GetInstance().playerRole = selectedRole;
        AfterChooseAction();
    }

    private void AfterChooseAction()
    {
        chooseCharacterCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

}
