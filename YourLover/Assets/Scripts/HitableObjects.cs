using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObjects : MonoBehaviour
{
    public int health = 1;

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
