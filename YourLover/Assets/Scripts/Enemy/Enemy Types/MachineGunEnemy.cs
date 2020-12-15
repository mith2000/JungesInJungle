using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnemy : Enemy
{
    [SerializeField]
    private float actionRange = 10f;

    [SerializeField]
    private float attackRate;

    [SerializeField]
    private int numberOfBullet;
    [SerializeField]
    private float minFactorAngle = 85f;
    [SerializeField]
    private float maxFactorAngle = 95f;
    private float rateOfFire = 7f;

    private float attackTime;

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
        if (target != null)
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
        triggerAttack = true;

        StartCoroutine(MachineGunReleaseBullet(targetTransform));
    }

    IEnumerator MachineGunReleaseBullet(Transform targetTransform)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            if (targetTransform == null) yield break;

            Vector2 direction = targetTransform.position - shotPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float randFactor = Random.Range(minFactorAngle, maxFactorAngle);
            Quaternion rotation = Quaternion.AngleAxis(angle - randFactor, Vector3.forward);
            shotPoint.rotation = rotation;

            Instantiate(bullet, shotPoint.position, shotPoint.rotation);

            yield return new WaitForSeconds(1 / rateOfFire);
        }
    }
}
