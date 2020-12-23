using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject stun;

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
