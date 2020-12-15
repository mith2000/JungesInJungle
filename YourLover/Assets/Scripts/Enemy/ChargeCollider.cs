using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCollider : MonoBehaviour
{
    public Transform ownerTransform;
    public int ownerDamage;
    public float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        if (ownerTransform != null)
        {
            transform.position = ownerTransform.position;
        }
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
