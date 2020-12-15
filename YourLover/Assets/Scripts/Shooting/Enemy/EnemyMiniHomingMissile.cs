using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMiniHomingMissile : EnemyHomingMissile
{
    private float delayFollowing = 0.5f;
    private float delayTime;

    public override void Start()
    {
        base.Start();
        delayTime = Time.time;
    }

    public override void FixedUpdate()
    {
        if (target != null)
        {
            if (Time.time - delayTime >= delayFollowing)
            {
                Vector2 direction = (Vector2)target.position - rb.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;

                rb.angularVelocity = -rotateAmount * rotateSpeed;

                rb.velocity = transform.up * speed;
            }
        }
        else
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(damage);
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
