using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header ("Range Settings")]
    [SerializeField] float actionRange = 10f;

    [SerializeField] float attackRate;

    float attackTime;

    public Transform shotPoint;
    public GameObject bullet;

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
            if (Vector2.Distance(transform.position, target.position) <= actionRange)
            {
                if (Time.time - attackTime >= 1 / attackRate)
                {
                    Shoot(target);
                    attackTime = Time.time;
                }
            }
        }
    }

    public void Shoot(Transform targetTransform)
    {
        AudioManager.GetInstance().Play("EnemyRange");
        triggerAttack = true;

        Vector2 direction = targetTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
    }
}
