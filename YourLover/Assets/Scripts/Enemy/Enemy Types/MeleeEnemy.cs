using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header ("Melee Settings")]
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackSpeed = 5f;
    [SerializeField] float attackRate = 2;
    [SerializeField] int attackDamage = 1;

    float attackTime;

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
        if (target != null &&
            targetPlayer.GetComponent<PlayerInfo>().currentHealth > 0)
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

        AudioManager.GetInstance().Play("EnemyMelee");
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
}
