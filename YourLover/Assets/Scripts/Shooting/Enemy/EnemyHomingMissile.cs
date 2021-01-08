using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile : HomingMissile
{
    public override void Start()
    {
        base.Start();
        AudioManager.GetInstance().Play("EnemyMissile");
        FindTarget();
    }

    public void FindTarget()
    {
        if (GameObject.FindGameObjectWithTag("Mercenary") == null)
        {
            if (GameObject.FindGameObjectWithTag("MonkeyClone") == null)
            {
                if (GameObject.FindGameObjectWithTag("Player") == null)
                {
                    target = null;
                }
                else
                {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
                }
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("MonkeyClone").transform;
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Mercenary").transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.GetInstance().Play("EnemyMissileHit");
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(damage);
                collision.GetComponent<PlayerInfo>().isGotSeriousInjury = true;
                collision.GetComponent<PlayerInfo>().RecoveryFromOther();
            }
            Explode();
        }
        else if (collision.CompareTag("Mercenary") || collision.CompareTag("MonkeyClone"))
        {
            collision.GetComponent<Entity>().TakeDamage(damage);
            Explode();
        }
        else if (collision.CompareTag("Block"))
        {
            Explode();
        }
    }
}
