using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowCrystal : MonoBehaviour
{
    [HideInInspector] public bool isGood = false;
    public int healthAmount = 1;
    public int loseHealthAmount = -2;
    public int armorAmount = 1;
    [SerializeField] float attackSpeedPercent = 10;
    [SerializeField] GameObject positiveVFX;
    [SerializeField] GameObject negativeVFX;

    private void Start()
    {
        if (Random.Range(0, 100) <= 50)
            isGood = true;
        else
            isGood = false;
    }

    public void DestroyPotion()
    {
        if (isGood)
        {
            Instantiate(positiveVFX, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(negativeVFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public float DecreaseDREPercent()
    {
        return 1f - attackSpeedPercent / 100f;
    }
}
