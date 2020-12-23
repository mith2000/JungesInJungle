using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : HitableObjects
{
    [SerializeField] GameObject dropper;

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                float posXRand = Random.Range(-2f, 2f);
                float posYRand = Random.Range(-2f, 2f);
                dropper.GetComponent<DropMachine>().Drop(transform.position + new Vector3(posXRand, posYRand, 0));
            }
            Destroy(gameObject);
        }
    }
}
