using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float bulletSpeed = 50;
    public float lifeTime = 5;

    public GameObject explodeVFX;

    Rigidbody2D physicBody;
    float rotSpeed = 720f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroyProj());
        physicBody = GetComponent<Rigidbody2D>();
        physicBody.velocity = transform.up * bulletSpeed;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }

    IEnumerator SelfDestroyProj()
    {
        yield return new WaitForSeconds(lifeTime);

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Instantiate(explodeVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            //Bounce Mechanic
            Vector2 blockNormal = collision.contacts[0].normal;
            Vector2 dir = Vector2.Reflect(physicBody.velocity, blockNormal).normalized;
            physicBody.velocity = dir * bulletSpeed;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            DestroyProjectile();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
