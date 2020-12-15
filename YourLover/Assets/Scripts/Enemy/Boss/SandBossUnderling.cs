using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DELAYED
public class SandBossUnderling : MonoBehaviour
{
    [SerializeField]
    private Enemy brain;
    [SerializeField]
    private Transform enemyGFX;
    [HideInInspector]
    public GameObject master;

    [SerializeField]
    private float changeStateRate = 0.3f;
    private float changeStateTime;

    [SerializeField]
    private float chaseRange = 20f;
    [SerializeField]
    private float stopRange = 2f;
    [SerializeField]
    private float chaseSpeed = 6f;

    private Transform randomPosition;

    public virtual void Update()
    {
        if (Time.time - changeStateTime >= 1 / changeStateRate)
        {
            ChangeState();
            changeStateTime = Time.time;
        }

        ChaseTarget();
    }

    private void ChangeState()
    {

    }

    private void RandomMove()
    {
        float randPosXToGo = Random.Range(-3f, 3f);
        float randPosYToGo = Random.Range(-3f, 3f);
        randomPosition.position = new Vector3(randPosXToGo, randPosYToGo);
    }

    public void ChaseTarget()
    {
        if (brain.target != null)
        {
            if (Vector2.Distance(transform.position, brain.target.position) < chaseRange &&
                Vector2.Distance(transform.position, brain.target.position) > stopRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, brain.target.position, chaseSpeed * Time.deltaTime);
            }

            if (transform.position.x < brain.target.position.x)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
