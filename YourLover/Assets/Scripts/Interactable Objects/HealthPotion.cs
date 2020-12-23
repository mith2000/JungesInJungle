using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healthAmount = 2;
    [SerializeField] GameObject healVFX;

    public void DestroyPotion()
    {
        Instantiate(healVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
