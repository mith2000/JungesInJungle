using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedCrystal : MonoBehaviour
{
    [SerializeField] float attackSpeedPercent = 20;
    [SerializeField] GameObject attackSpeedUpVFX;

    public void DestroyPotion()
    {
        Instantiate(attackSpeedUpVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //Decrease 'Delay Regen Energy' to make player regen faster
    public float DecreaseDREPercent()
    {
        return 1f - attackSpeedPercent / 100f;
    }
}
