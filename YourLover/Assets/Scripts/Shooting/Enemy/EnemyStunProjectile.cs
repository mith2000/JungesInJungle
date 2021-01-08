using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunProjectile : EnemyProjectile
{
    [Header ("Stun Settings")]
    [SerializeField] float stunDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isReverse)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.GetInstance().Play("PlayerStun");
                collision.GetComponent<PlayerInfo>().TakeDamage(damage);
                collision.GetComponent<PlayerController>().isStunned = true;
                collision.GetComponent<PlayerController>().UnStunFromOther(stunDuration);
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
