using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicProjectile : MonoBehaviour
{
    [Header ("Projectile Settings")]
    [SerializeField] protected int damage = 5;
    [SerializeField] public float speed = 15;
    [SerializeField] protected float lifeTime = 3;
    [SerializeField] GameObject explosionVFX;

    public virtual void Start()
    {
        StartCoroutine(SelfDestroyProj());
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public IEnumerator SelfDestroyProj()
    {
        yield return new WaitForSeconds(lifeTime);

        Explode();
    }

    public void Explode()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Explode();
        }
        if (collision.CompareTag("GoldMine"))
        {
            collision.GetComponent<GoldMine>().TakeDamage(damage);
            Explode();
        }
        if (collision.CompareTag("Block"))
        {
            Explode();
        }
        if (collision.CompareTag("Crafts"))
        {
            collision.GetComponent<HitableObjects>().TakeDamage(damage);
            Explode();
        }
    }
}
