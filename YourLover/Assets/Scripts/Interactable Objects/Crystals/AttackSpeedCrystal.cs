using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedCrystal : MonoBehaviour
{
    [SerializeField]
    private float attackSpeedPercent = 20;
    [SerializeField]
    private GameObject attackSpeedUpVFX;

    public void DestroyPotion()
    {
        Instantiate(attackSpeedUpVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public float DecreaseDREPercent()
    {
        return 1f - attackSpeedPercent / 100f;
    }
}
