using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterEnemy : Enemy
{
    [Header ("Caster Settings")]
    [SerializeField] float actionRange = 25f;
    [SerializeField] float attackRate;

    float attackTime;

    [SerializeField] GameObject castObject;

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
                    Cast(target);
                    attackTime = Time.time;
                }
            }
        }
    }

    public void Cast(Transform targetTransform)
    {
        AudioManager.GetInstance().Play("MeteorFall");
        triggerAttack = true;

        Instantiate(castObject, targetTransform.position, Quaternion.identity);
    }
}
