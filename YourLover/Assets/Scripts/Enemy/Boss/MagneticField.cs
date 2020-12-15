using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    private float lifeTime = 3.25f;

    private int maxNumberOfTick = 3;
    private int currentTick = 0;
    private bool canTick = true;
    private float tickRate = 2f;
    private float tickTime;

    private float slowRate = .75f;
    private float stunDuration = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tickTime = Time.time;
            collision.GetComponent<PlayerController>().SlowFromOther(slowRate, (float)maxNumberOfTick / tickRate);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - tickTime >= 1 / tickRate)
            {
                if (canTick)
                {
                    currentTick++;
                    tickTime = Time.time;
                }
            }

            if (currentTick == maxNumberOfTick)
            {
                canTick = false;
                currentTick = 0;
                collision.GetComponent<PlayerController>().isStunned = true;
                collision.GetComponent<PlayerController>().UnStunFromOther(stunDuration);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentTick = 0;
            canTick = true;
            collision.GetComponent<PlayerController>().UnslowFromOther();
        }
    }
}
