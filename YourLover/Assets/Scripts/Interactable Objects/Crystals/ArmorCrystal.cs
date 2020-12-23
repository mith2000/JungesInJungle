using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCrystal : MonoBehaviour
{
    public int armorAmount = 1;
    [SerializeField] GameObject armorUpVFX;

    public void DestroyPotion()
    {
        Instantiate(armorUpVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
