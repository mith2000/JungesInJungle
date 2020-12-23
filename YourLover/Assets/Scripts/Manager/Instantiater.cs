using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    [SerializeField] CharacterChoice choicer;

    [Header ("Characters Instantiate Position")]
    [SerializeField] Transform monkeyInstantiatePosition;
    [SerializeField] Transform catInstantiatePosition;

    [Header ("Cameras")]
    [SerializeField] CinemachineStateDrivenCamera stateDrivenCamera;
    [SerializeField] CinemachineVirtualCamera idleCamera;
    [SerializeField] CinemachineVirtualCamera walkCamera;

    [SerializeField] bool isPlayScene;

    private void Start()
    {
        if (isPlayScene)
            GameMasterInstantiateCharacter();
    }

    public void ChoicerInstantiateCharacter()
    {
        if (choicer != null)
        switch (choicer.selectedRole)
        {
            case GameMaster.CharacterRole.Monkey:
                Instantiate(PrefabContainer.GetInstance().monkeyPrefab, monkeyInstantiatePosition.position, Quaternion.identity);
                break;
            case GameMaster.CharacterRole.Cat:
                Instantiate(PrefabContainer.GetInstance().catPrefab, catInstantiatePosition.position, Quaternion.identity);
                break;
            default:
                break;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        stateDrivenCamera.m_AnimatedTarget = player.GetComponent<Animator>();
        idleCamera.m_Follow = player.transform;
        walkCamera.m_Follow = player.transform;
    }

    public void GameMasterInstantiateCharacter()
    {
        switch (GameMaster.GetInstance().playerRole)
        {
            case GameMaster.CharacterRole.Monkey:
                Instantiate(PrefabContainer.GetInstance().monkeyPrefab, monkeyInstantiatePosition.position, Quaternion.identity);
                break;
            case GameMaster.CharacterRole.Cat:
                Instantiate(PrefabContainer.GetInstance().catPrefab, catInstantiatePosition.position, Quaternion.identity);
                break;
            default:
                break;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        stateDrivenCamera.m_AnimatedTarget = player.GetComponent<Animator>();
        idleCamera.m_Follow = player.transform;
        walkCamera.m_Follow = player.transform;
    }
}
