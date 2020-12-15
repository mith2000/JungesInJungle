﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : Enemy
{
    [SerializeField]
    private float attackRange = 2f;
    [SerializeField]
    private float attackSpeed = 5f;
    [SerializeField]
    private float attackRate = 2;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private GameObject childPrefab;
    private bool splitted = false;

    private float attackTime;

    public override void Start()
    {
        base.Start();
        FindPlayer();

        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public override void Update()
    {
        base.Update();
        AttackTarget();
    }

    private void AttackTarget()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= attackRange)
            {
                if (Time.time - attackTime >= 1 / attackRate)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        if (target.gameObject.CompareTag("Player"))
        {
            target.GetComponent<PlayerInfo>().TakeDamage(attackDamage);
        }
        else
        {
            target.GetComponent<Entity>().TakeDamage(attackDamage);
        }

        triggerAttack = true;

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = target.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += attackSpeed * Time.deltaTime;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

    public override void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Min(currentHealth - damageAmount, maxHealth);
        healthBar.value = maxHealth - currentHealth;
        if (!splitted)
        { 
            if (currentHealth <= maxHealth / 2)
            {
                GameObject child1 = Instantiate(childPrefab, transform.position, transform.rotation);
                child1.transform.position = Vector2.MoveTowards(child1.transform.position, child1.transform.position + new Vector3(5, 0, 0), 5f * Time.deltaTime);
                GameObject child2 = Instantiate(childPrefab, transform.position, transform.rotation);
                child2.transform.position = Vector2.MoveTowards(child2.transform.position, child2.transform.position + new Vector3(5, 0, 0), 5f * Time.deltaTime);
                splitted = true;
                Destroy(gameObject);
            } 
        }
    }
}