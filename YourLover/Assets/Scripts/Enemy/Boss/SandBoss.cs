using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoss : MonoBehaviour
{
    [SerializeField] Boss brain;

    float attackRate = 0.3f;
    float attackTime;

    [Header ("Radiate Skill")]
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject hakiBullet;
    int hakiBulletCount = 12;
    float delayPerBullet = 0.1f;

    [Header ("Magnetic Field Skill")]
    [SerializeField] GameObject magneticField;

    [Header ("Summon Skill")]
    [SerializeField] GameObject underlingPrefab;
    int minNumberOfSummon = 2;
    int maxNumberOfSummon = 4;

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
            PlaceMagneticField(brain.target);
        }
        else if (rand >= 70)
        {
            StartCoroutine(SummonUnderling());
        }
        else
        {
            StartCoroutine(Haki(brain.target));
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
        for (int i = 0; i < hakiBulletCount; i++)
        {
            AudioManager.GetInstance().Play("EnemyRange");
            float addedOffset = (i - (hakiBulletCount / 2)) * (360 / hakiBulletCount);
            newRot = Quaternion.Euler(shotPoint.transform.eulerAngles.x,
                shotPoint.transform.eulerAngles.y,
                shotPoint.transform.eulerAngles.z + addedOffset);
            Instantiate(hakiBullet, shotPoint.position, newRot);

            yield return new WaitForSeconds(delayPerBullet);
        }
    }

    private void PlaceMagneticField(Transform targetTransform)
    {
        AudioManager.GetInstance().Play("BossMagnetic");
        brain.anim.SetTrigger("Stun");

        Instantiate(magneticField, targetTransform.position, Quaternion.identity);
    }

    IEnumerator SummonUnderling()
    {
        brain.anim.SetTrigger("Summon");

        int randNumberOfSummon = Random.Range(minNumberOfSummon, maxNumberOfSummon + 1);
        for (int i = 0; i < randNumberOfSummon; i++)
        {
            AudioManager.GetInstance().Play("EnemySummon");
            Vector3 offsetPos = Vector3.zero;
            if (Random.Range(0, 100) < 50)
                offsetPos.x = Random.Range(-1f, -1f);
            else
                offsetPos.x = Random.Range(1f, 1f);
            if (Random.Range(0, 100) < 50)
                offsetPos.y = Random.Range(-1f, -1f);
            else
                offsetPos.y = Random.Range(1f, 1f);

            Instantiate(underlingPrefab, transform.position + offsetPos, Quaternion.identity);

            yield return new WaitForSeconds(1 / (float)randNumberOfSummon); //1 second to summon all
        }
    }
}
