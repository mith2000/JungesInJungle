using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonkeyClone : Entity
{
    [SerializeField]
    private float speed = 3f;
    private float addSpeed = 1f;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackSpeed = 5f;
    [SerializeField]
    private float attackRate = .5f;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private GameObject gfx;

    private float attackTime;

    private Rigidbody2D physicBody;
    private Animator anim;

    private GameObject target;
    private GameObject parent;
    private float followParentDistance = 2f;
    private float maxDistanceBtwParent = 12.5f;
    private float flipYAmount = 180f;

    private void Awake()
    {
        //Cache References
        physicBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InvokeRepeating("FindClosestEnemy", 0f, 2f);
        parent = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Update()
    {
        base.Update();
        Walking();
        FlipSprite();
    }

    private void Walking()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > attackRange)
            {
                anim.SetBool("Walk", true);
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time - attackTime >= 1 / attackRate)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time;
                }
            }
        }
        else
        {
            if (parent != null)
            {
                if (Vector2.Distance(transform.position, parent.transform.position) < maxDistanceBtwParent)
                {
                    if (Vector2.Distance(transform.position, parent.transform.position) > followParentDistance)
                    {
                        anim.SetBool("Walk", true);
                        transform.position = Vector2.MoveTowards(transform.position, parent.transform.position, speed * Time.deltaTime);
                    }
                }
                else
                {
                    transform.position = parent.transform.position;
                }
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }

        physicBody.velocity = Vector2.zero;
    }

    public void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If no enemies found at all directly return nothing
        // This happens if there simply is no object tagged "Enemy" in the scene
        if (enemies.Length == 0)
        {
            target = null;
            return;
        }

        GameObject closest;

        // If there is only exactly one anyway skip the rest and return it directly
        if (enemies.Length == 1)
        {
            closest = enemies[0];
            //target.transform.position = closest.transform.position;
            target = closest;
            return;
        }


        // Otherwise: Take the enemies
        closest = enemies.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First();

        //target.transform.position = closest.transform.position;
        //Debug.Log(target.transform.position);

        target = closest;
        return;
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        target.GetComponent<Enemy>().TakeDamage(attackDamage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = target.transform.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += attackSpeed * Time.deltaTime;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

    private void FlipSprite()
    {
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                gfx.transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                gfx.transform.rotation = new Quaternion(0, flipYAmount, 0, 1);
            }
        }
        else
        {
            if (parent != null)
            {
                if (parent.transform.position.x > transform.position.x)
                {
                    gfx.transform.rotation = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    gfx.transform.rotation = new Quaternion(0, flipYAmount, 0, 1);
                }
            }
        }
    }

}
