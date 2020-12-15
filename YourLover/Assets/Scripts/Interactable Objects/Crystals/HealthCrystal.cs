using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrystal : MonoBehaviour
{
    public int healthAmount = 2;
    [SerializeField]
    private GameObject healVFX;

    public void DestroyPotion()
    {
        Instantiate(healVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
