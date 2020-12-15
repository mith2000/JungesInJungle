using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    [SerializeField]
    private CharacterChoice choicer;

    [SerializeField]
    private Transform monkeyInstantiatePosition;
    [SerializeField]
    private Transform catInstantiatePosition;

    [SerializeField]
    private CinemachineStateDrivenCamera stateDrivenCamera;
    [SerializeField]
    private CinemachineVirtualCamera idleCamera;
    [SerializeField]
    private CinemachineVirtualCamera walkCamera;

    [SerializeField]
    private bool isPlayScene;

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
