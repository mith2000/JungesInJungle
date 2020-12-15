using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameMaster.GetInstance().enter = true;
        }
    }
}
