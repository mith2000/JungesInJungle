using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrbanBoss : MonoBehaviour
{
    [SerializeField] Boss brain;

    float attackRate = 0.2f;
    float attackTime;

    [Header ("Quarter Laser Skill")]
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject laser;

    [Header ("Multimissile Skill")]
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform[] missileShotPoints;

    [Header ("Laser Army Skill")]
    [HideInInspector] Vector3 originPosition;
    [SerializeField] GameObject dropdownLaserPrefab;
    [SerializeField] GameObject crossLaserPrefab;
    [SerializeField] GameObject sideLaserPrefab;
    GameObject dropdownLaser;
    GameObject crossLaser;
    GameObject sideLaser;

    private void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        if (DialogSystem.GetInstance().isInDialog)
        {
            attackTime = Time.time; //not action when dialoging
            return;
        }

        AttackTarget();
    }

    private void FixedUpdate()
    {
        if (dropdownLaser != null)
        {
            dropdownLaser.transform.Translate(Vector2.down * 5.5f * Time.deltaTime);
        }
        if (sideLaser != null)
        {
            sideLaser.transform.Translate(Vector2.right * 11.5f * Time.deltaTime);
        }
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
            StartCoroutine(ShootLaser(brain.target.transform));
        }
        else if (rand >= 70)
        {
            ShootMissiles(brain.target.transform);
        }
        else
        {
            StartCoroutine(CallSetupLaser());
        }
    }

    IEnumerator ShootLaser(Transform targetTransform)
    {
        AudioManager.GetInstance().Play("BossLaser");
        brain.anim.SetTrigger("Laser");

        laser.SetActive(true);

        Vector2 direction = targetTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;
        Quaternion newRot = transform.rotation;
        int rand = Random.Range(0, 100);
        for (int i = -45; i < 45; i++)
        {
            if (rand <= 50)
            {
                newRot = Quaternion.Euler(shotPoint.transform.eulerAngles.x,
                    shotPoint.transform.eulerAngles.y,
                    shotPoint.transform.eulerAngles.z + i);
            }
            else
            {
                newRot = Quaternion.Euler(shotPoint.transform.eulerAngles.x,
                    shotPoint.transform.eulerAngles.y,
                    shotPoint.transform.eulerAngles.z - i);
            }
            laser.transform.rotation = newRot;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void ShootMissiles(Transform targetTransform)
    {
        AudioManager.GetInstance().Play("EnemyMissile");
        brain.anim.SetTrigger("Missile");

        Vector2 direction = targetTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        foreach (Transform trans in missileShotPoints)
        {
            trans.rotation = rotation;
            Instantiate(missilePrefab, trans.position, trans.rotation);
        }
    }

    IEnumerator CallSetupLaser()
    {
        brain.anim.SetTrigger("Call");

        int rand1 = Random.Range(0, 100);
        Quaternion dropRotate = Quaternion.identity;
        if (rand1 < 50)
            dropRotate = Quaternion.Euler(180, 0, 0);
        AudioManager.GetInstance().Play("BossLaser");
        dropdownLaser = Instantiate(dropdownLaserPrefab, originPosition, dropRotate);
        Destroy(dropdownLaser, 1f);
        yield return new WaitForSeconds(1f);

        int rand2 = Random.Range(0, 100);
        Quaternion crossRotate = Quaternion.identity;
        if (rand2 < 50)
            crossRotate = Quaternion.Euler(180, 0, 0);
        AudioManager.GetInstance().Play("BossLaser");
        crossLaser = Instantiate(crossLaserPrefab, originPosition, crossRotate);
        Destroy(crossLaser, .5f);
        yield return new WaitForSeconds(.5f);

        int rand3 = Random.Range(0, 100);
        Quaternion sideRotate = Quaternion.identity;
        if (rand3 < 50)
            sideRotate = Quaternion.Euler(0, 180, 0);
        AudioManager.GetInstance().Play("BossLaser");
        sideLaser = Instantiate(sideLaserPrefab, originPosition, sideRotate);
        Destroy(sideLaser, 1f);
        yield return new WaitForSeconds(1f);
    }

    //For animator
    public void TurnOffLaser()
    {
        laser.SetActive(false);
    }
}
