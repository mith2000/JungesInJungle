using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [Header ("Health")]
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField]
    protected Slider healthBar;

    //protected int savedHealth;
    //bool canShowDamage = true;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = 0;
        ///savedHealth = currentHealth;
    }

    //public virtual void Update()
    //{
    //    //if (savedHealth > currentHealth)
    //    //{
    //    //    if (canShowDamage)
    //    //    {
    //    //        ShowDamage(savedHealth - currentHealth);
    //    //        canShowDamage = false;
    //    //        StartCoroutine(WaitNextTimeForShowDamage());
    //    //    }
    //    //}
    //    //StartCoroutine(DelaySaveHealth());
    //}

    //IEnumerator WaitNextTimeForShowDamage()
    //{
    //    yield return new WaitForSeconds(.3f);
    //    canShowDamage = true;
    //}

    //IEnumerator DelaySaveHealth()
    //{
    //    yield return new WaitForSeconds(.5f);
    //    savedHealth = currentHealth;
    //}

    public void ShowDamage(string damageValue)
    {
        GameObject dmgInfo = Instantiate(PrefabContainer.GetInstance().damageInfoPrefab, transform.position, Quaternion.identity);
        dmgInfo.GetComponent<DamageInfo>().damage = damageValue;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Min(currentHealth - damageAmount, maxHealth);
        healthBar.value = maxHealth - currentHealth;
        ShowDamage(damageAmount.ToString());
        if (currentHealth <= 0)
        {
            //Die action
            Destroy(gameObject);
        }
    }
}
