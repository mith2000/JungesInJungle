using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Entity
{
    [Header ("Enemy Settings")]
    [HideInInspector] public Transform targetPlayer;
    [HideInInspector] public Transform target;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject deathBody;
    [SerializeField] DropMachine dropper;

    [HideInInspector] public bool triggerAttack = false;

    #region Finding Target
    public void FindPlayer()
    {
        StartCoroutine(DelayFindPlayer());
    }

    IEnumerator DelayFindPlayer()
    {
        yield return new WaitForSeconds(.5f);
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FindTarget()
    {
        //if (FindClosestMercenary() != null)
        //    target = FindClosestMercenary().transform;
        //else if (targetPlayer != null)
        //    target = targetPlayer;

        if (FindClosestMercenary() != null && targetPlayer != null)
        {
            if (Vector2.Distance(transform.position, FindClosestMercenary().transform.position) <=
                Vector2.Distance(transform.position, targetPlayer.position))
            {
                target = FindClosestMercenary().transform;
            }
            else
            {
                target = targetPlayer;
            }
        }
        else if (targetPlayer != null)
            target = targetPlayer;
    }

    public GameObject FindClosestMercenary()
    {
        GameObject[] mercenaries;
        if (GameObject.FindGameObjectWithTag("MonkeyClone") != null)
        {
            mercenaries = GameObject.FindGameObjectsWithTag("MonkeyClone");
        }
        else
        {
            mercenaries = GameObject.FindGameObjectsWithTag("Mercenary");
        }

        // If no enemies found at all directly return nothing
        // This happens if there simply is no object tagged "Enemy" in the scene
        if (mercenaries.Length == 0)
        {
            return null;
        }

        GameObject closest;

        // If there is only exactly one anyway skip the rest and return it directly
        if (mercenaries.Length == 1)
        {
            closest = mercenaries[0];
            //target.transform.position = closest.transform.position;
            return closest;
        }


        // Otherwise: Take the enemies
        closest = mercenaries.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();

        //target.transform.position = closest.transform.position;
        //Debug.Log(target.transform.position);

        return closest;
    }
    #endregion

    public virtual void Update()
    {
        CheckDied();
    }

    public override void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Min(currentHealth - damageAmount, maxHealth);
        healthBar.value = maxHealth - currentHealth;
        ShowDamage(damageAmount.ToString());
    }

    public void CheckDied()
    {
        if (currentHealth <= 0)
        {
            DieAction();
        }
    }

    public virtual void DieAction()
    {
        //Die action //Instantiate die effect
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Instantiate(deathBody, transform.position, transform.rotation);
        //Drop reward
        dropper.Drop(transform.position);
        Destroy(gameObject);
    }
}
