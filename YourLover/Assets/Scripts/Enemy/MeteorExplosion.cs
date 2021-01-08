using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplosion : MonoBehaviour
{
    public int ownerDamage;

    public void DestroySelf()
    {
        AudioManager.GetInstance().Play("BombExplode");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(ownerDamage);
                collision.GetComponent<PlayerInfo>().isGotSeriousInjury = true;
                collision.GetComponent<PlayerInfo>().RecoveryFromOther();
            }
        }
    }
}
