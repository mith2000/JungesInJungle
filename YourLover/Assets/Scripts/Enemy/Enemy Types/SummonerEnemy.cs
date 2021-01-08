using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    [Header ("Summon Settings")]
    [SerializeField] float summonRate;

    float summonTime;

    [SerializeField] GameObject[] summonedPrefabs;

    public override void Start()
    {
        base.Start();
        FindPlayer();

        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public override void Update()
    {
        base.Update();
        SummonWhenTargetAlive();
    }

    private void SummonWhenTargetAlive()
    {
        if (target != null &&
            targetPlayer.GetComponent<PlayerInfo>().currentHealth > 0)
        {
            if (Time.time - summonTime >= 1 / summonRate)
            {
                Summon();
                summonTime = Time.time;
            }
        }
    }

    public void Summon()
    {
        AudioManager.GetInstance().Play("EnemySummon");
        triggerAttack = true;

        Vector3 summonedObjAddPos = Vector3.zero;
        if (Random.Range(0, 100) < 50)
            summonedObjAddPos.x = Random.Range(-2f, -1f);
        else
            summonedObjAddPos.x = Random.Range(2f, 1f);
        if (Random.Range(0, 100) < 50)
            summonedObjAddPos.y = Random.Range(-2f, -1f);
        else
            summonedObjAddPos.y = Random.Range(2f, 1f);

        int rand = Random.Range(0, summonedPrefabs.Length);
        Instantiate(summonedPrefabs[rand], transform.position + summonedObjAddPos, Quaternion.identity);
    }
}
