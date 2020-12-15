using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    [SerializeField]
    private int damage = 8;
    [SerializeField]
    private GameObject explosion;

    public void EndFall()
    {
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.GetComponent<MeteorExplosion>().ownerDamage = damage;
        Destroy(gameObject);
    }
}
