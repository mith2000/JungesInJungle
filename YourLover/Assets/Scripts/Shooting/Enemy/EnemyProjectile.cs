using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyProjectile : BasicProjectile
{
    [HideInInspector] public bool isReverse = false;

    public override void Start()
    {
        base.Start();
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
