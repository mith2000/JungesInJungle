using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    int bombDamage = 5;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(bombDamage);
                collision.GetComponent<PlayerInfo>().isGotSeriousInjury = true;
                collision.GetComponent<PlayerInfo>().RecoveryFromOther();
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(bombDamage * 2);
        }
        else if (collision.CompareTag("Mercenary") || collision.CompareTag("MonkeyClone"))
        {
            collision.GetComponent<Entity>().TakeDamage(bombDamage * 2);
        }
    }
}
