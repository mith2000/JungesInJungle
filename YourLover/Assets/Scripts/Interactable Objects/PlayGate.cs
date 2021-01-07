using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.GetInstance().Play("GameStart");
            StartCoroutine(DelayEnter());
        }
    }

    IEnumerator DelayEnter()
    {
        yield return new WaitForSeconds(.5f);
        GameMaster.GetInstance().StartGame();
    }
}
