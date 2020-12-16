using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    [SerializeField]
    private Boss brain;

    private float attackRate = 0.3f;
    private float attackTime;

    [SerializeField]
    private GameObject castObject;
    private int numberOfMeteorPerCast = 4;
    private float meteorRatePerCast = 10;

    [SerializeField]
    private Transform shotPoint;
    [SerializeField]
    private GameObject shotgunBullet;
    private int shotgunBulletCount = 4;
    private float spread = 10f;
    private int shootWave = 2;
    private float waveRatePerShoot = 10;

    [SerializeField]
    private GameObject hakiBullet;
    private int hakiBulletCount = 12;
    private int hakiWave = 3;
    private float waveRatePerHaki = 5;

    void Update()
    {
        if (DialogSystem.GetInstance().isInDialog)
        {
            attackTime = Time.time; //not action when dialoging
            return; 
        }

        AttackTarget();
    }

    private void AttackTarget()
    {
        if (brain.target != null)
        {
            if (Time.time - attackTime >= 1 / attackRate)
            {
                ChooseAction();
                attackTime = Time.time;
            }
        }
    }

    private void ChooseAction()
    {
        int rand = Random.Range(0, 100);
        if (rand <= 30)
        {
            StartCoroutine(CastComboMeteor(brain.target));
        }
        else if (rand >= 70)
        {
            StartCoroutine(ShotgunShoot(brain.target));
        }
        else
        {
            StartCoroutine(Haki(brain.target));
        }
    }

    IEnumerator CastComboMeteor(Transform targetTransform)
    {
        brain.anim.SetTrigger("Cast");
        for (int i = 0; i < numberOfMeteorPerCast; i++)
        {
            if (targetTransform == null) yield break;

            Vector3 offsetPos = Vector3.zero;
            if (Random.Range(0, 100) < 50)
                offsetPos.x = Random.Range(-1f, -1f);
            else
                offsetPos.x = Random.Range(1f, 1f);
            if (Random.Range(0, 100) < 50)
                offsetPos.y = Random.Range(-1f, -1f);
            else
                offsetPos.y = Random.Range(1f, 1f);

            Instantiate(castObject, targetTransform.position + offsetPos, Quaternion.identity);

            yield return new WaitForSeconds(1 / meteorRatePerCast);
        }
    }

    IEnumerator ShotgunShoot(Transform targetTransform)
    {
        brain.anim.SetTrigger("Shoot");

        Vector2 direction = targetTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;
        Quaternion newRot = transform.rotation;
        for (int j = 0; j < shootWave; j++)
        {
            for (int i = 0; i < shotgunBulletCount; i++)
            {
                float addedOffset = (i - (shotgunBulletCount / 2)) * spread;
                newRot = Quaternion.Euler(shotPoint.transform.eulerAngles.x,
                    shotPoint.transform.eulerAngles.y,
                    shotPoint.transform.eulerAngles.z + addedOffset);
                Instantiate(shotgunBullet, shotPoint.position, newRot);
            }
            yield return new WaitForSeconds(1 / waveRatePerShoot);
        }
    }

    IEnumerator Haki(Transform targetTransform)
    {
        brain.anim.SetTrigger("Haki");

        Vector2 direction = targetTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;
        Quaternion newRot = transform.rotation;
        for (int j = 0; j < hakiWave; j++)
        {
            for (int i = 0; i < hakiBulletCount; i++)
            {
                float addedOffset = (i - (hakiBulletCount / 2)) * (360 / hakiBulletCount);
                newRot = Quaternion.Euler(shotPoint.transform.eulerAngles.x,
                    shotPoint.transform.eulerAngles.y,
                    shotPoint.transform.eulerAngles.z + addedOffset);
                Instantiate(hakiBullet, shotPoint.position, newRot);
            }
            yield return new WaitForSeconds(1 / waveRatePerHaki);
        }
    }
}
