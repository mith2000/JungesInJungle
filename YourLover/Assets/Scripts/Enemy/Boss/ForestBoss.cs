using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    [SerializeField] Boss brain;

    float attackRate = 0.3f;
    float attackTime;

    [Header ("Cast Meteor Skill")]
    [SerializeField] GameObject castObject;
    int numberOfMeteorPerCast = 4;
    float meteorRatePerCast = 10;

    [Header ("Shotgun Skill")]
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject shotgunBullet;
    int shotgunBulletCount = 4;
    float spread = 10f;
    int shootWave = 2;
    float waveRatePerShoot = 10;

    [Header ("Radiate Skill")]
    [SerializeField] GameObject hakiBullet;
    int hakiBulletCount = 12;
    int hakiWave = 3;
    float waveRatePerHaki = 5;

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
        if (brain.target != null &&
            brain.targetPlayer.GetComponent<PlayerInfo>().currentHealth > 0)
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

            AudioManager.GetInstance().Play("MeteorFall");
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
            AudioManager.GetInstance().Play("BossShotgun");
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
            AudioManager.GetInstance().Play("BossHaki");
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
