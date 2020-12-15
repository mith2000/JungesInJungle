using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowdownProjectile : EnemyProjectile
{
    public override void Start()
    {
        base.Start();
        InvokeRepeating("SlowdownBullet", 0f, 0.1f);
    }

    private void SlowdownBullet()
    {
        if (speed > 0)
            speed -= (1 / (lifeTime + 2f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReverse)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(damage);
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

        if (isReverse)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
                Explode();
            }
            else if (collision.CompareTag("Block"))
            {
                Explode();
            }
        }
    }
}
