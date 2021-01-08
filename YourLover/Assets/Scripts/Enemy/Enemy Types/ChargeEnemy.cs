using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChargeEnemy : Enemy
{
    [Header ("Chase and Charge Settings")]
    [SerializeField] Transform enemyGFX;
    [SerializeField] float chaseRange = 20f;
    [SerializeField] float stopRange = 4f;
    [SerializeField] float chaseSpeed = 4f;

    [SerializeField] float attackRange = 10f;
    [SerializeField] float attackSpeed = 5f;
    [SerializeField] float attackRate = .3f;
    [SerializeField] int attackDamage = 5;
    [SerializeField] ChargeCollider chargeColPrefab;

    float attackTime;
    bool charging = false;
    Vector2 chargeDirection;
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        FindPlayer();

        StartCoroutine(FocusPlayer());
    }

    IEnumerator FocusPlayer()
    {
        yield return new WaitForSeconds(0.6f);
        target = targetPlayer;
    }

    public override void Update()
    {
        base.Update();
        if (target == null ||
            targetPlayer.GetComponent<PlayerInfo>().currentHealth <= 0) return;

        Chase();
        Charge();
        FlipSprite();
    }

    private void Chase()
    {
        if (!charging)
        {
            if (Vector2.Distance(transform.position, target.position) < chaseRange &&
                Vector2.Distance(transform.position, target.position) > stopRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            }
        }
    }

    private void Charge()
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

    IEnumerator Attack()
    {
        AudioManager.GetInstance().Play("EnemyCharge");
        triggerAttack = true;
        charging = true;

        Vector2 direction = (new Vector2(target.position.x, target.position.y) - body.position).normalized;
        chargeDirection = direction * attackSpeed * Time.fixedDeltaTime;

        ChargeCollider vfx = Instantiate(chargeColPrefab, transform.position, Quaternion.identity);
        vfx.ownerTransform = transform;
        vfx.ownerDamage = attackDamage;
        vfx.lifeTime = .7f;

        yield return new WaitForSeconds(.7f);
        charging = false;
    }

    private void FixedUpdate()
    {
        if (charging)
            body.MovePosition(body.position + chargeDirection);
    }

    private void FlipSprite()
    {
        if (transform.position.x < target.position.x)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}
