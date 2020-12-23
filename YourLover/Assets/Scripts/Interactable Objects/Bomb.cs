using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    float lifeTime = 3f;
    public bool isTaken = false;
    public Transform ownerTransform;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private void FixedUpdate()
    {
        if (isTaken)
        {
            if (ownerTransform != null)
            {
                transform.position = ownerTransform.position;
            }
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(lifeTime);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
