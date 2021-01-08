using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    float lifeTime = 3.25f;

    int maxNumberOfTick = 3;
    int currentTick = 0;
    bool canTick = true;
    float tickRate = 2f;
    float tickTime;

    float slowRate = .75f;
    float stunDuration = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    //Slow player right when instantiated
    //Duration: time to stack full tick 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tickTime = Time.time;
            collision.GetComponent<PlayerController>().SlowFromOther(slowRate, (float)maxNumberOfTick / tickRate);
        }
    }

    //When player colliding, stack tick 
    //If tick up to max tick -> stun player inside
    //NOT GOOD FOR MANY PLAYERS
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
                AudioManager.GetInstance().Play("PlayerStun");
                canTick = false;
                currentTick = 0;
                collision.GetComponent<PlayerController>().isStunned = true;
                collision.GetComponent<PlayerController>().UnStunFromOther(stunDuration);
            }
        }
    }

    //Reset tick and unslow player when get out successfully
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
