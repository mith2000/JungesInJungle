using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameObject stun;

    void Update()
    {
        CheckStun();
    }

    void CheckStun()
    {
        if (player.isStunned)
        {
            stun.GetComponent<Animator>().SetBool("Active", true);
        }
        else
        {
            stun.GetComponent<Animator>().SetBool("Active", false);
        }
    }
}
