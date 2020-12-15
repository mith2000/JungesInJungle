using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    [SerializeField]
    private Transform enemyGFX;
    [SerializeField]
    private float chaseRange = 20f;
    [SerializeField]
    private float stopRange = 4f;
    [SerializeField]
    private float chaseSpeed = 4f;

    [SerializeField]
    private float attackRange = 10f;
    [SerializeField]
    private float attackSpeed = 5f;
    [SerializeField]
    private float attackRate = .3f;
    [SerializeField]
    private int attackDamage = 5;
    [SerializeField]
    private ChargeCollider chargeColPrefab;

    private float attackTime;
    private bool charging = false;
    private Vector2 chargeDirection;
    private Rigidbody2D body;

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
        if (target == null) return;

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
